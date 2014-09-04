using UnityEngine;
using System.Collections;

public class DestructibleWalls : MonoBehaviour {



	float hitAtTime;

	// Use this for initialization
	void Start () 
	{
		hitAtTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{

			if(Time.time - hitAtTime > 4)
			{
				Destroy(gameObject.GetComponent<Rigidbody>());
				if(Time.time - hitAtTime > 5)
				{
					Destroy(gameObject);
				}
				
			}



	}
}
