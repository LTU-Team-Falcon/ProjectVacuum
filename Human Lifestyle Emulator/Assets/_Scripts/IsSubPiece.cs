using UnityEngine;
using System.Collections;

public class IsSubPiece : MonoBehaviour 
{
	public float breakForce = 10f;
	public float breakTorque = 10f;

	// Use this for initialization
	void Start () 
	{	

		//To Do: Search for Glue!!!
		FixedJoint bond = transform.parent.gameObject.AddComponent<FixedJoint>();
		bond.breakForce = this.breakForce;
		bond.breakTorque = this.breakTorque;
		bond.connectedBody = this.rigidbody;
	}


	void AddJoint()
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}