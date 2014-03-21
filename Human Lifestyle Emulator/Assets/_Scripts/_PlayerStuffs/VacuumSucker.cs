using UnityEngine;
using System.Collections;

public class VacuumSucker : MonoBehaviour 
{
	public float maxFlyVelocity = 20f;
	
	public float suckPow;
	
	public float suckDist;
	[HideInInspector]
	public float suckDist2;
	
	// Use this for initialization
	void Start ()
	{
		suckDist2 = suckDist*suckDist;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Suckable")
		{
			if(col.gameObject.GetComponent<GetSucked>().canGetSucked)
			{
				Destroy(col.gameObject);
			}
		}
	}
}
