using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VacuumPuncher : MonoBehaviour 
{
	//public bool hasHit = false;
	
	private List<GameObject> hasMadeCollision = new List<GameObject>();
	private VacuumController vacController;

	// Use this for initialization
	void Start () 
	{
		vacController = transform.parent.gameObject.GetComponent<VacuumController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void ClearHasHits()
	{
		hasMadeCollision.Clear();
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(vacController.isPunching && col.tag == "Suckable" && col.GetComponent<GetSucked>().hasSuckyParent)
		{//checks if it hasn't already hit anything
			
			foreach(GameObject hit in hasMadeCollision)
			{ //checks to see if we've already collided with this object during this punch
				if(hit == col.gameObject)
				{
					return;
				}
			}

			//never reaches here if its already been hit this punch
			GetSucked colGetSucked = col.gameObject.GetComponent<GetSucked>();
			colGetSucked.damage += vacController.vacSucker.suckPotential;
			
			
			if(colGetSucked.damage >= colGetSucked.resistance)
			{
				colGetSucked.SplitFromParent();
			}
		}
	}

}
