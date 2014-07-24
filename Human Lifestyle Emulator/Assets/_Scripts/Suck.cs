using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Suck : MonoBehaviour 
{
	public bool IsSucking;

	public List<GameObject> PossibleSuckers = new List<GameObject>();

	VacuumSucker vacuumSucker;
	// Use this for initialization
	void Start () 
	{
		vacuumSucker = transform.parent.GetComponent<VacuumSucker>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Suckable")
		{
			PossibleSuckers.Add(col.gameObject);
		}
	}

	void OnTriggerExit(Collider col)
	{
		PossibleSuckers.Remove(col.gameObject);
	}

	void FixedUpdate()
	{
			GameObject i = PossibleSuckers[0];
			SuckThis(i);
	}

	void SuckThis(GameObject parObj)
	{
		Vector3 force = new Vector3(0,0,0);
		Vector3 relVec = parObj.rigidbody.ClosestPointOnBounds(vacuumSucker.transform.position) - vacuumSucker.transform.position;
		
		float relDist = relVec.magnitude;
			
		force = relVec.normalized*vacuumSucker.suckPow;

		force *=1;// Mathf.Pow((vacuumSucker.suckDist - relDist), vacuumSucker.suckFalloff)/(Mathf.Pow (vacuumSucker.suckDist, vacuumSucker.suckFalloff));

		float AngleDot = Vector3.Dot(vacuumSucker.transform.forward.normalized, relVec.normalized);	//AngleDot sets the "focus" of the force
		AngleDot -= 5f/6f;
		AngleDot = Mathf.Max( AngleDot, 0) * 12f;
		force *= AngleDot;
		if(parObj.rigidbody.mass > 1)
		{
			force = Vector3.Scale(force, new Vector3(1, parObj.rigidbody.mass,1));
		}

		
		parObj.rigidbody.AddForceAtPosition(-force,relVec + vacuumSucker.transform.position);
	}
}
