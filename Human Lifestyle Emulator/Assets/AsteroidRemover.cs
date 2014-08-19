using UnityEngine;
using System.Collections;

public class AsteroidRemover : MonoBehaviour 
{
	public int age =0;
	int deathTime = 2000;
	int overTime = 300;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.position.magnitude >1000)
		{
			Destroy(gameObject);
		}
		if(age >= deathTime)
		{
			float blipper = 1 - (float)(age - deathTime)/(float)overTime;

			blipper = Mathf.Cos( 1/( Mathf.Pow(blipper , 2) + 0.01f ) );
			if(blipper > 0.5f)
			{
				this.renderer.enabled = false;
			}
			else
			{
				this.renderer.enabled = true;
			}



			if(age >= deathTime + overTime)
			{
				Destroy(gameObject);
			}
		}

		if(rigidbody.velocity.sqrMagnitude >= 0.01f)
		{
			gameObject.tag = "Fired";
		}
		else if(rigidbody.angularVelocity.sqrMagnitude > 0.01f)
		{
			gameObject.tag = "Fired";
		}
		else
		{
			gameObject.tag = "Untagged";
		}

		age++;
	}
}
