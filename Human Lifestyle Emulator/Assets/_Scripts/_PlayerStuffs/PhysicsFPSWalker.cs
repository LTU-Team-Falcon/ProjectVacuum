using UnityEngine;
using System.Collections;

public class PhysicsFPSWalker : MonoBehaviour 
{
	XinputHandler control;	
	GameManager gameManager;
	public Rigidbody Rigidbody;
	public bool debug =false;
	// These variables are for adjusting in the inspector how the object behaves 
	public float maxSpeed  = 10;
	public float force     = 8;
	public float jumpSpeed = 7;
	public float Increase = .07f;

	//[HideInInspector]
	public AudioClip Run;
	public bool Running;
	//[HideInInspector]
	public AudioClip Jump;
	public bool Jumping;
	//[HideInInspector]
	public AudioClip Dash;
	public bool Dashing;
	
	// These variables are there for use by the script and don't need to be edited
	public int state = 0;
	public int state2 = 0;
	public bool grounded = false;
	private float jumpLimit = 1000;
	private float dashLimit = 1000;

	[HideInInspector]
	public int JumpCount = 0;
	[HideInInspector]
	public int TimeTillDash = 77;
	[HideInInspector]
	public bool SuperJumpActive = false;

	private float XZmovementMag;

	bool pressedJump = false;
	bool pressedDash = false;
	
	void Awake ()
	{ 	// Don't let the Physics Engine rotate this physics object so it doesn't fall over when running
		if(transform.parent.gameObject.GetComponent<XinputHandler>() != null)
		{
			control = transform.parent.gameObject.GetComponent<XinputHandler>();
		}

		rigidbody.freezeRotation = true;
	}
	
	void OnCollisionEnter ()
	{	// This part detects whether or not the object is grounded and stores it in a variable >> Got this code online; totally do not understand it
		state ++;
		if(state > 0)
		{
			grounded = true;
		}
	}
	
	void OnCollisionStay()
	{
		state2++;
	}

	void OnCollisionExit ()
	{
		state --;
		if(state < 1)
		{
			grounded = false;
			state = 0;
		}
	}

	//ToDo: make movement feel good.
	void FixedUpdate ()
	{	// This is called every physics frame



		Vector3 velXZ =  new Vector3(rigidbody.velocity.x,0,rigidbody.velocity.z);
		float XZmag = velXZ.magnitude;
		XZmovementMag = XZmag;

		Rigidbody = gameObject.GetComponent<Rigidbody> ();
		
		float speedMod = (maxSpeed - XZmag)/(maxSpeed); //calculates a relative difference in the speed of the player


		Vector2 mouseInput = control.GetLeftStick()*force;
		float horz = mouseInput.normalized.x * force;
		float vert = mouseInput.normalized.y * force;
				
		//Should we give the robotTreads?
		//ToDo: fix movement so it doesnt determine your control based on the total max speed;  
		//rather, on the individual components; allowing you to adjust your velocity if you are going the max speed
		if(grounded)
		{		// If the object is grounded and isn't moving at the max speed or higher apply force to move it
			//rigidbody.velocity = new Vector3(rigidbody.velocity.x,0,rigidbody.velocity.z);
			if(vert !=0)
			{
				rigidbody.AddForce (new Vector3(transform.forward.x, 0, transform.forward.z) * vert * speedMod);	
				Rigidbody.drag = 0.2f;
				Running = true;
				Dashing = false;
				Jumping = false;
			}
			else
			{//slows the player down if they aren't applying a force
				//rigidbody.AddForce(transform.rotation * new Vector3(0,0,-velXZ.normalized.z) * speedMod); 
				rigidbody.AddForce(transform.rotation * (new Vector3(0,0,-velXZ.z) * rigidbody.mass)); 
				//print(-velXZ.z + " z ");

				rigidbody.drag += Increase;

			}
			
			if(horz != 0)
			{
				rigidbody.AddForce (new Vector3(transform.right.x, 0, transform.right.z) * horz * speedMod);
				Rigidbody.drag = 0.2f;
				Running = true;
				Dashing = false;
				Jumping = false;
			}
			else
			{//slows the player down if they aren't applying a force

			//	rigidbody.AddForce(transform.rotation * new Vector3( - velXZ.x,0,0) * rigidbody.mass);
				rigidbody.AddForce(transform.rotation * new Vector3( - velXZ.normalized.x,0,0) * 0.3f);

				//print(- velXZ.normalized.x + " x ");
				rigidbody.drag += Increase;
			}




		}
		else
		if(!grounded)
		{ //if object is in the air; give you control over it slightly
			rigidbody.drag = 0.5f;
			if(Time.frameCount%15 == 0)
			{
				rigidbody.velocity += Vector3.down;
			}

			if(vert != 0)
			{
				rigidbody.AddForce (new Vector3(transform.forward.x, 0, transform.forward.z) * vert * speedMod * 0.7f);
			}
			
			if(horz != 0)
			{
				rigidbody.AddForce (new Vector3(transform.right.x, 0, transform.right.z) * horz * speedMod * 0.7f);
			}
		}
		
		// This part is for jumping. I only let jump force be applied every 10 physics frames so
		// the player can't somehow get a huge velocity due to multiple jumps in a very short time
		if(jumpLimit < 10000) jumpLimit ++;
		if(dashLimit < 10000) dashLimit ++;


		if(control.GetButton("A") && (grounded) && jumpLimit >= 20)
		{
			rigidbody.drag = 1;
			rigidbody.velocity = rigidbody.velocity + (Vector3.up * jumpSpeed *2f);
			jumpLimit = 0;
			audio.clip = Jump;
			audio.Play();
		}
		else if(control.GetButton("A") && SuperJumpActive == true && jumpLimit >=20 && JumpCount < 2)
		{
			rigidbody.drag = 1;
			rigidbody.velocity = rigidbody.velocity + (Vector3.up * jumpSpeed *2f);
			jumpLimit = 0;
			JumpCount++;
			audio.clip = Jump;
			audio.Play();
		}
		else if(control.GetButton("LB")&& dashLimit >= TimeTillDash)
		{
			rigidbody.drag = 1;
			Vector3 DashDirection = new Vector3(transform.forward.x, 0 , transform.forward.z) * control.GetLeftStick().y;	
			DashDirection += new Vector3(transform.right.x, 0 , transform.right.z) * control.GetLeftStick().x;	
			DashDirection *= 27f;
			DashDirection += new Vector3(0, 3f, 0);

			rigidbody.velocity = ( DashDirection );
			dashLimit = 0;
			if (TimeTillDash == 77)
			{
				if(!grounded)
				{
					dashLimit = -17f;
				}
			}
			audio.clip = Dash;
			audio.Play();
		}	
		else if(control.GetButton("RB") && !grounded)
		{
			rigidbody.drag = 1;
			rigidbody.velocity += Vector3.down;
			audio.clip = Dash;
			audio.Play();
		}

//		Debug.Log (rigidbody.velocity.y);

	}


	void Update()
	{
		state2 = 0;
		pressedJump = control.GetButtonDown("A");
		pressedDash = control.GetButtonDown("LB");

		if (grounded && rigidbody.velocity.magnitude <= .3f)
		{
			audio.clip = Run;
			audio.Play();
		}
		else if (!grounded)
		{

		}

	}

	

}
