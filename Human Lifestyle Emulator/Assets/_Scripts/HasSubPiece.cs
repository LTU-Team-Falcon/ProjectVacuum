using UnityEngine;
using System.Collections;

public class HasSubPiece : MonoBehaviour 
{
	public float breakForce = 10f;
	public float breakTorque = 10f;

	public float Spring = 0f;
	public float Bounce = 0f;
	public float Damper = 0f;

	CharacterJoint bond;
	GameObject glue;
	// Use this for initialization
	void Start () 
	{
		glue = Instantiate(Resources.Load("GlueObject") as GameObject, transform.position, transform.rotation) as GameObject;
		glue.transform.parent = this.transform;
		for(int i = 0; i < transform.childCount; i++)
		{
			if(transform.GetChild(i).gameObject.GetComponent<GetSucked>() != null)
			{
				AddJoint(i);
			}
		}
	}


	void AddJoint(int parIndex)
	{
		bond = glue.AddComponent<CharacterJoint>();
		bond.breakForce = this.breakForce;
		bond.breakTorque = this.breakTorque;
		bond.connectedBody = transform.GetChild(parIndex).gameObject.rigidbody;

		SoftJointLimit softy = new SoftJointLimit();
		softy.limit = 0f;
		bond.swing1Limit = softy;
		bond.swing2Limit = softy;
		bond.highTwistLimit = softy;
		bond.lowTwistLimit = softy;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}