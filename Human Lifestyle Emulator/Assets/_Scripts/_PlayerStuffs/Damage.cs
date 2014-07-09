using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	
	public float damageCounter;
	// Use this for initialization
	void Start () {
		
		damageCounter = 1;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Fired")
		{
			col.gameObject.tag = "Suckable";
			Vector3 direction =  transform.position - col.transform.position;
			float Factor = col.rigidbody.mass * col.relativeVelocity.magnitude * damageCounter * 750;
			rigidbody.AddForce(direction.normalized * Factor);
			damageCounter += col.rigidbody.mass * 5;
		}
	}
}