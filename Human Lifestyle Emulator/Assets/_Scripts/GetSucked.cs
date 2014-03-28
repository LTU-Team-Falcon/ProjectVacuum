using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetSucked : MonoBehaviour 
{
	[HideInInspector]
	public bool hasSuckyParent = false;
	
	public float attachForce; //not implemented yet.
	
	
	public bool isDebugging = false;
	TextMesh debugTextMesh;
	
	[HideInInspector]
	public Vector3 origScale;
	[HideInInspector]
	public float origSize;
	[HideInInspector]
	public float origHealth;
	
	private VacuumSucker vacuumSucker;
	private Vector3 velocity;
	private Vector3 force;
	
	[HideInInspector]
	public bool canGetSucked = true;
	
	public float resistance = 0f; //strength to resist sucking
	[HideInInspector]
	public float health = 0f; //amount of punching it can withstand >> Goes with Damage
	
	private float realMass;//used to recreate the rigidbody if the rigidbody is destroyed
	
	public float size = 0f;//once it has 0 health; this determines how fast it shrinks in the intake;
	
	[HideInInspector]
	public float damage = 0;

	public List<float> debugList;

	// Use this for initialization
	void Start () 
	{
		realMass = rigidbody.mass;
		reCalcVar();
		
		
		if(transform.parent.gameObject.GetComponent<GetSucked>() != null)
		{//if its parents are suckable, it disables this one.
			hasSuckyParent = true;
			canGetSucked = false;
			transform.parent.rigidbody.mass += this.rigidbody.mass;
			Destroy(rigidbody);
		}
		
		
		

		
		
		vacuumSucker = GameObject.FindGameObjectWithTag("Vacuum").GetComponent("VacuumSucker") as VacuumSucker;
		gameObject.tag = "Suckable";
		
		origScale = transform.localScale;
		origSize = size;
		origHealth = health;

	}
	
	public void SplitFromParent()
	{ //is called when the object has recieved enough force to where it breaks off from its parent
		if(hasSuckyParent == true)
		{
			transform.parent = null;
		}
		gameObject.AddComponent<Rigidbody>().mass = realMass;
		hasSuckyParent = false;
		canGetSucked = true;
		this.UnLockRigid();
	}
	
	public void reCalcVar()
	{
		if(!(size > 0)) size = rigidbody.mass;//temp 
		if(!(resistance > 0)) resistance = rigidbody.mass*2;
		if(!(health > 0)) health = resistance;
	}
	
	// Update is called once per physics frame
	void FixedUpdate ()
	{//determines the force withwhich it is pulled towards the sucker.
		if(damage > 0) {	damage -= 0.3f;	}
		
		if(canGetSucked)
		{	
			Vector3 relVec = rigidbody.ClosestPointOnBounds(vacuumSucker.transform.position);
			relVec = relVec - vacuumSucker.transform.position;
			
			float relDist2 = relVec.sqrMagnitude;
			
			if(relDist2 < vacuumSucker.suckDist2)
			{
				force = relVec.normalized*vacuumSucker.suckPow;
				force *= (vacuumSucker.suckDist2 - relDist2)/vacuumSucker.suckDist2;
				force *= Time.fixedDeltaTime*-50f;//multiplies it by the amount of time between each frame
				rigidbody.AddForceAtPosition(force,relVec + vacuumSucker.transform.position);
			}
		}
	}

	public void LockRigid()
	{
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		rigidbody.isKinematic = true;
	}
	
	public void FreezeRigid()
	{
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}
	
	public void UnLockRigid()
	{
		rigidbody.constraints = RigidbodyConstraints.None;
		rigidbody.isKinematic = false;
	}
	
	
	
	void OnGUI()
	{//this is all stuff for debugging; right now its set to display text of the current force on the object; can be changed to whatever pretty easily though
		if(isDebugging)
		{
			if(this.isDebugging && debugTextMesh == null)
			{
				GameObject textObj = vacuumSucker.playerObj.GetComponent<GameManager>().createNewDebugTextObj();
				textObj.transform.position = this.transform.position + Vector3.up * 1.5f;
				textObj.transform.parent = this.transform;
				
				debugTextMesh = textObj.GetComponent<TextMesh>();
			}
			
			if(debugList.Count == 10)
			{
				float sol = 0;
				foreach(float d in debugList)
				{
					sol += d;
				}
				
				if(isDebugging)	{	debugTextMesh.text = "f = " + (float)(sol/debugList.Count);	}
				debugList.Clear();
			}
			else
			{
				debugList.Add(force.magnitude);
			}
		}
	}
}
