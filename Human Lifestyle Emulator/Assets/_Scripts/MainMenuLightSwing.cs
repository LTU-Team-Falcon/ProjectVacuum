using UnityEngine;
using System.Collections;

public class MainMenuLightSwing : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		float force = Random.value*10+20;
		
		float r1 = Random.value;
				
		rigidbody.AddForceAtPosition(new Vector3(r1, 0, 1-r1) * force, transform.GetChild(0).position);
		Invoke("Twist", 2f);
	}
	
	void Twist()
	{
		float force = Random.value*2+5;
		float r1 = Random.value;
		
		rigidbody.AddForceAtPosition(new Vector3(r1, 0, 1-r1) * force, transform.GetChild(0).position);
	}
	
	void FixedUpdate()
	{
		rigidbody.AddForceAtPosition(Vector3.down * rigidbody.mass, transform.GetChild(0).position);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
}
