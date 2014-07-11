using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseForce : MonoBehaviour 
{

	XinputHandler control;	

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float maximumY = 60F;


	public GameObject XObj;
	public GameObject YObj;

	HingeJoint Xhinge;
	HingeJoint Yhinge;

	GameObject camObj;
	GameObject playaObj;

	float springCounter = 0;


	void Start ()
	{
		if(transform.parent.gameObject.GetComponent<XinputHandler>() != null)
		{
			control = transform.parent.gameObject.GetComponent<XinputHandler>();
		}



		Screen.lockCursor = true;
		Screen.showCursor = false;

		Xhinge = YObj.GetComponent<HingeJoint>();
		Yhinge = XObj.GetComponent<HingeJoint>();

		/*if(turgetTransform== null)
		{
			turgetTransform = GameObject.Find("Main Camera").transform;
		}*/
	}


	void FixedUpdate ()
	{
		Vector2 rightStick = control.GetRightStick();
		float aimAccel = Mathf.Abs(rightStick.magnitude);
		rightStick *= aimAccel;
		float rotationX = rightStick.x * sensitivityX;
		float rotationY = rightStick.y * sensitivityX;;


		Vector3 roty = new Vector3((-rotationY) , (rotationX) , 0);

		JointMotor newMot = new JointMotor();
		newMot.force = sensitivityX;
		newMot.targetVelocity = -roty.x;
		Xhinge.motor = newMot;

		newMot.force = sensitivityY;
		newMot.targetVelocity = -roty.y;
		Yhinge.motor = newMot;
	}

	void LateUpdate()
	{

		//rigidbody.angularVelocity =  Vector3.Scale(rigidbody.angularVelocity, new Vector3(1,1,0));
		//transform.localEulerAngles = Vector3.Scale(transform.localEulerAngles, new Vector3(1,1,0));

/*		rigidbody.angularVelocity =  Vector3.Scale(rigidbody.angularVelocity, new Vector3(1,1,0));
		transform.eulerAngles = Vector3.Scale(transform.eulerAngles, new Vector3(1,1,0));
*/
/*		print(rigidbody.angularVelocity);
		print(transform.eulerAngles);
*/	}
}

//Crap
/*if (axes == RotationAxes.MouseXAndY)
		{
			Vector3 target = turgetTransform.eulerAngles;
			if(target.x > 180)
			{
				//target.x -= 360;
			}

			Vector3 cury = transform.eulerAngles;
			if(cury.x > 180)
			{
				cury.x -= 360;
			}
			
			if(cury.z != 0)
			{
				//rigidbody.angularVelocity = Vector3.Scale(rigidbody.angularVelocity ,new Vector3(1,1,0));
				
				//transform.eulerAngles = Vector3.Scale(transform.eulerAngles,new Vector3(1,1,0));
			}

			float rotationX = Input.GetAxis("Mouse X") * sensitivityX;

			float rotationY = Input.GetAxis("Mouse Y") * sensitivityY;


			//Vector3 roty = new Vector3((-rotationY - target.x) , (rotationX - target.y) , 0);
		


		/*	if(cury.y < -180)
			{
				cury.y += 360;
			}
			else if(cury.y > 180)
			{
				cury.y -= 360;
			}*/


/*			if(target.y < -180)
			{
				target.y += 360;
			}
			else if(target.y > 180)
			{
				target.y -= 360;
			}

			Vector3 dify = new Vector3(cury.x - target.x, cury.y - target.y,0); //new Vector3(target.x - cury.x,target.y - cury.y,0);

/*			if(dify.x < -180)
			{
				dify.x += 360;
			}
			else if(dify.x > 180)
			{
				dify.x -= 360;
			}

			float difyMag = dify.magnitude;
			dify *= 0.02f;
			Vector3 roty = new Vector3((-rotationY) , (rotationX) , 0/* target.z - cury.z);

			//Vector3 dify = new Vector3(cury.x - target.x, cury.y - target.y,0);//cury - target;//



			float sensei = sensitivityX;
			//testAxis1.transform.localEulerAngles += dify;

			rigidbody.AddTorque(roty);

	//		print("target:" + target + "  cury:" + cury);
		
		//	print("roty:" + roty);

	//		print("dify:" + dify + "   difyMag:" + difyMag);



			if(rigidbody.angularVelocity.magnitude > difyMag)
			{
				rigidbody.angularVelocity = rigidbody.angularVelocity.normalized * difyMag;
			}


		}
	/*	else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
			Debug.Log("if 2");
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			Debug.Log("if 3");
		}*/