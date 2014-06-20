using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC_AI : MonoBehaviour {

	public bool FoundClosest;
	public GameObject[] Suckables;
	public GameObject ClosestSuckable;

	private int count = 0;

	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {

		if (count > 350)
		{
			ClosestSuckable = Suckables[1];
		}
		if (ClosestSuckable == null)
		{
			count = 0;
			rigidbody.isKinematic = true;
		}
		else
		{
			rigidbody.isKinematic = false;
		}


		if (FoundClosest == true)
		{
			float dist = Vector3.Distance(transform.position,ClosestSuckable.transform.position) ;
			if(dist > 5f)
			{
				transform.position += Vector3.Scale((ClosestSuckable.transform.position - transform.position), new Vector3(1,0,1)) * 0.7f*Time.deltaTime;

				transform.LookAt(ClosestSuckable.transform.position);
				transform.eulerAngles = Vector3.Scale(new Vector3(0,1,0), transform.eulerAngles);
			}
			if (dist <= 20f)
			{
				transform.LookAt(ClosestSuckable.transform.position);
				transform.eulerAngles = Vector3.Scale(new Vector3(0,1,0), transform.eulerAngles);

				ClosestSuckable.rigidbody.AddForce( Vector3.Normalize( transform.position - ClosestSuckable.transform.position) * 30f * dist/10 + Vector3.Normalize( transform.position - ClosestSuckable.transform.position) * 20f);
			}
		}

	}

	void FixedUpdate () {
	

		Suckables = GameObject.FindGameObjectsWithTag("Suckable");

		if (Suckables.Length > 0)
		{
			ClosestSuckable = Suckables[0];
			float dist = Vector3.Distance(transform.position, Suckables[0].transform.position);

			for ( int i = 0; i < Suckables.Length; i++)
			{
				float tempdist = Vector3.Distance(transform.position, Suckables[i].transform.position);
				if (tempdist < dist)
					ClosestSuckable = Suckables[i];
			}
	
			FoundClosest = true;

		}

	}

}
