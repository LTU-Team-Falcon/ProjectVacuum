using UnityEngine;
using System.Collections;

public class DestructibleWalls : MonoBehaviour {



	bool hit = false;
	int hitAtTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(hit)
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

	public void SelfDestruct()
	{
		hit = true;
		hitAtTime = (int)Time.time;
	}
}
