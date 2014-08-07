using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour {
	
	public Transform target;
	public float distance = 5.0f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;
	
	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;
	
	public float distanceMin = 5f;
	public float distanceMax = 10f;

	public List<GameObject> Players= new List<GameObject>();

	public XinputHandler control;

	float x = 0.0f;
	float y = 0.0f;

	public int count = 0;
	public int PlayerSize;

	// Use this for initialization
	void Start () {
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;

		foreach (GameObject Player in GameObject.FindGameObjectsWithTag("Player"))
		{
			Players.Add(Player);
		}
		PlayerSize = Players.Count;	
		target = Players[count].transform;

		distanceMax = 7;
		distanceMin = 7;
	}


	void Update () {

		if (count >= PlayerSize)
		{
			count = 0;	
		}
		
		if(control.GetButtonDown("A"))
		{
			Players.Clear();
			foreach (GameObject Player in GameObject.FindGameObjectsWithTag("Player"))
			{
				Players.Add(Player);
			}
			PlayerSize = Players.Count;
			target = Players[count].transform;	
			count++;
		}
	}
	
	void LateUpdate () {
		if (target) {
			Vector2 rightStick = control.GetRightStick();

			x += rightStick.x * xSpeed * distance * 0.02f;
			y -= rightStick.y * ySpeed * 0.02f;
			
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			
			Quaternion rotation = Quaternion.Euler(y, x, 0);
			
			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);
			
			RaycastHit hit;
			if (Physics.Linecast (target.position, transform.position, out hit)) {
				distance -=  hit.distance;
			}
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;
			
			transform.rotation = rotation;
			transform.position = position;
			
		}
		
	}


	/*void FixedUpdate ()
	{
		Vector2 rightStick = control.GetRightStick();
		float aimAccel = Mathf.Abs(rightStick.magnitude);
		rightStick *= aimAccel;
		float rotationX = rightStick.x * xSpeed;
		float rotationY = rightStick.y * xSpeed;;
		
		
		Vector3 roty = new Vector3((-rotationY) , (rotationX) , 0);
	}*/
	
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
	
	
}