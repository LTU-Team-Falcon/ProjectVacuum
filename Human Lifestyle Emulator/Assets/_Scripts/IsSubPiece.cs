using UnityEngine;
using System.Collections;

public class IsSubPiece : MonoBehaviour 
{
	public float breakForce = 10f;
	public float breakTorque = 10f;

	CharacterJoint bond;
	// Use this for initialization
	void Start () 
	{	

		//To Do: Search for Glue!!!
		bond = transform.parent.gameObject.AddComponent<CharacterJoint>();
		bond.breakForce = this.breakForce;
		bond.breakTorque = this.breakTorque;
		bond.connectedBody = this.rigidbody;
		
		
		SoftJointLimit softy = new SoftJointLimit();
		softy.limit = 0f;
		bond.swing1Limit = softy;
		bond.swing2Limit = softy;
		bond.highTwistLimit = softy;
		bond.lowTwistLimit = softy;
	}


	void AddJoint()
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}