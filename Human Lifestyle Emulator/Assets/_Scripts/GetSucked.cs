using UnityEngine;
using System.Collections;

public class GetSucked : MonoBehaviour 
{
	
	private VacuumSucker vacuumSucker;
	private Vector3 velocity;
	private Vector3 force;
	
	public bool canGetSucked = true;

	// Use this for initialization
	void Start () 
	{
		rigidbody.constraints = RigidbodyConstraints.None;
		vacuumSucker = GameObject.FindGameObjectWithTag("Vacuum").GetComponent("VacuumSucker") as VacuumSucker;
		gameObject.tag = "Suckable";
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(canGetSucked)
		{
			
			Vector3 relVec = rigidbody.ClosestPointOnBounds(vacuumSucker.transform.position);
			relVec = relVec - vacuumSucker.transform.position;//new Vector3(transform.position.x - vacuumSucker.transform.position.x, transform.position.y - vacuumSucker.gameObject.transform.position.y, transform.position.z - vacuumSucker.gameObject.transform.position.z);
			
			float relDist2 = relVec.sqrMagnitude;
			if(relDist2 < vacuumSucker.suckDist2)
			{
				
				force = relVec.normalized*vacuumSucker.suckPow;
				force *= (vacuumSucker.suckDist2 - relDist2)/vacuumSucker.suckDist2;
				force *= Time.deltaTime*-20;
				rigidbody.AddForceAtPosition(force,relVec + vacuumSucker.transform.position);
				
			}
		}
	}
}
