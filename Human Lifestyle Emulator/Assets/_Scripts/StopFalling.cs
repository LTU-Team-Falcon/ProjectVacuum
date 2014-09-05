using UnityEngine;
using System.Collections;

public class StopFalling : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;

	}
	
	// Update is called once per frame
	void Update () {

	
	}


	void OnCollisionEnter (Collision col)
	{
		//if hit by fired object
		if (col.gameObject.tag == "Fired") 
		{
			rigidbody.constraints = RigidbodyConstraints.None;
		}
	}


	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.name == "SuckerTruely") 
		{
			if(col.gameObject.GetComponentInParent<VacuumSucker>().suckPow != 0)
			{
				rigidbody.constraints = RigidbodyConstraints.None;
			}
		}

	}
}
