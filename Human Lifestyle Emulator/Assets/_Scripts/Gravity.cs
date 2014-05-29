using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gravity : MonoBehaviour {

	public static float range = 1000;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () 
	{
		Collider[] cols  = Physics.OverlapSphere(transform.position, range); 
		List<Rigidbody> rbs = new List<Rigidbody>();
		
		foreach(Collider c in cols)
		{
			Rigidbody rb = c.attachedRigidbody;
			if(rb != null && rb != rigidbody && !rbs.Contains(rb))
			{
				rbs.Add(rb);
				Vector3 offset = transform.position - c.transform.position;
				rb.AddForce( offset / offset.sqrMagnitude * rigidbody.mass);
			}
		}
	}
}
