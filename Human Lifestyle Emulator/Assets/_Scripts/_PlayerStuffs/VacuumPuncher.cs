using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VacuumPuncher : MonoBehaviour 
{
	//public bool hasHit = false;
	
	private List<GameObject> hasMadeCollision = new List<GameObject>();
	[HideInInspector]
	public VacuumController vacController;

	public float punchLength = 0.2f;
	private float punchCoolDown = 0.2f;

	public bool isPunching = false;
	public bool canPunchAgain = true;


	public void initiatePunch()
	{
		canPunchAgain = false;
		isPunching = true;
		Invoke("DonePunching", punchLength);
	}

	void DonePunching()
	{
		vacController.rigidbody.AddExplosionForce(vacController.vacSucker.suckPow/2f, transform.position, 1.5f);
		isPunching = false;
		vacController.transform.localPosition = vacController.defaultLocPos;
		Invoke("EnablePunch", punchCoolDown);
		//vacSucker.isSucking = false;
	}


	void ClearHasHits()
	{
		hasMadeCollision.Clear();
	}

	void EnablePunch()
	{
		canPunchAgain = true;
		//vacSucker.isSucking = true;
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Suckable" && col.GetComponent<GetSucked>().hasSuckyParent)//vacController.isPunching && 
		{//checks if it hasn't already hit anything
			
			foreach(GameObject hit in hasMadeCollision)
			{ //checks to see if we've already collided with this object during this punch
				if(hit == col.gameObject)
				{
					return;
				}
			}

			//never reaches here if its already been hit this punch
		//	GetSucked colGetSucked = col.gameObject.GetComponent<GetSucked>();
		}
	}

}
