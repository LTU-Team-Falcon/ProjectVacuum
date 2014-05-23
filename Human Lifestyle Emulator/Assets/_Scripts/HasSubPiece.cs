using UnityEngine;
using System.Collections;

public class HasSubPiece : MonoBehaviour 
{
	public float breakForce = 10f;
	public float breakTorque = 10f;
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
		FixedJoint bond = glue.AddComponent<FixedJoint>();
		bond.breakForce = this.breakForce;
		bond.breakTorque = this.breakTorque;
		bond.connectedBody = transform.GetChild(parIndex).gameObject.rigidbody;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}