using UnityEngine;
using System.Collections;

public class Forcefield : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag != "Suckable")
		{
			Physics.IgnoreCollision(col.collider, collider);
		}

	}
}
