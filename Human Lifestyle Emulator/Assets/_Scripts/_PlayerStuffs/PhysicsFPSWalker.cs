using UnityEngine;
using System.Collections;

public class PhysicsFPSWalker : MonoBehaviour {
	
	
	
	// These variables are for adjusting in the inspector how the object behaves 
	public float maxSpeed  = 7;
	public float force     = 8;
	public float jumpSpeed = 5;
	
	// These variables are there for use by the script and don't need to be edited
	private int state = 0;
	private bool grounded = false;
	private float jumpLimit = 0;
	
	private float XZmovementMag;
	
	public float jumpTime = 0;
	private float lastJump =0;
	
	void Awake ()
	{ 	// Don't let the Physics Engine rotate this physics object so it doesn't fall over when running
		
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
	
	
	void OnCollisionExit ()
	{
		state --;
		if(state < 1)
		{
			grounded = false;
			state = 0;
		}
	}
	
	
	public virtual bool jump
	{
		get 
		{
			return Input.GetButton ("Jump");
		}
	}
	
	public virtual float horizontal
	{
		get
		{
			return Input.GetAxis("Horizontal") * force;
		} 
	} 
	public virtual float vertical
	{
		get
		{
			return Input.GetAxis("Vertical") * force;
		} 
	}
	
	//ToDo: make movement feel good.
	void FixedUpdate ()
	{	// This is called every physics frame

		Vector3 velXZ =  new Vector3(rigidbody.velocity.x,0,rigidbody.velocity.z);
		float XZmag = velXZ.magnitude;
		XZmovementMag = XZmag;
		
		
		float speedMod = (maxSpeed - XZmag)/(maxSpeed); //calculates a relative difference in the speed of the player


		Vector2 mouseInput = new Vector2(horizontal,vertical);
		float horz = mouseInput.normalized.x * force;
		float vert = mouseInput.normalized.y * force;
				
		//Should we give the robotTreads?
		//ToDo: fix movement so it doesnt determine your control based on the total max speed;  
		//rather, on the individual components; allowing you to adjust your velocity if you are going the max speed
		if(grounded)
		{		// If the object is grounded and isn't moving at the max speed or higher apply force to move it
			
			if(vert !=0)
			{
				rigidbody.AddForce (new Vector3(transform.forward.x, 0, transform.forward.z) * vert * speedMod);				
			}
			else
			{//slows the player down if they aren't applying a force
				//rigidbody.AddForce(transform.rotation * new Vector3(0,0,-velXZ.normalized.z) * speedMod); 
				rigidbody.AddForce(transform.rotation * (new Vector3(0,0,-velXZ.z) * rigidbody.mass)); 
			}
			
			if(horz != 0)
			{
				rigidbody.AddForce (new Vector3(transform.right.x, 0, transform.right.z) * horz * speedMod);
				
			}
			else
			{//slows the player down if they aren't applying a force
				rigidbody.AddForce(transform.rotation * new Vector3( - velXZ.normalized.x,0,0) * speedMod * 0.3f); 
			}
			
		}
		else
		if(!grounded)
		{ //if object is in the air; give you control over it slightly
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
		if(jumpLimit < 10) jumpLimit ++;
		if(lastJump < jumpTime) lastJump++;
		
		if(jump && (grounded || lastJump == jumpTime) && jumpLimit >= 10)
		{
			rigidbody.velocity = rigidbody.velocity + (Vector3.up * jumpSpeed);
			jumpLimit = 0;
			lastJump = 0;
		}
	}
	
	void OnGUI()
	{
		string sped = "Speed: " + (float)(Mathf.Round(XZmovementMag*10f)/10f);
		GUI.Box (new Rect (10,10,100,20), sped);
	}
}
