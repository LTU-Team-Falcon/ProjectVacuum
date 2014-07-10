using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	
	private float damageCounter;

	private int Lives;
	// Use this for initialization
	void Start () {
		
		damageCounter = 1;
		Lives = 3;
		
		
	}
	
	// Update is called once per frame
	void Update () {

		//on death
		if (transform.parent.transform.position.y < 10)
		{
			OnDeath();
		}

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

	void OnDeath ()
	{
		Debug.Log("Dead");
		transform.position = new Vector3(0,15,0);
		//Lives--;

	}
}