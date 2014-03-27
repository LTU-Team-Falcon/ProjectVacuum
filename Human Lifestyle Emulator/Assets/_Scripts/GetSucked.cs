using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetSucked : MonoBehaviour 
{
	//[HideInInspector]
	private bool hasSuckyParent = false;
	public float attachForce;
	
	
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
	
	public float resistance = 0f; //strength to resist defences
	[HideInInspector]
	public float health = 0f; //amount of punching it can withstand >> Goes with Damage
	public float size = 0f;
	
	[HideInInspector]
	public float damage = 0;

	public List<float> debugList;

	// Use this for initialization
	void Start () 
	{
		
		if(!(size > 0)) size = rigidbody.mass;//temp 
		if(!(resistance > 0)) resistance = rigidbody.mass*2;
		if(!(health > 0)) health = resistance;
		
		
		vacuumSucker = GameObject.FindGameObjectWithTag("Vacuum").GetComponent("VacuumSucker") as VacuumSucker;
		gameObject.tag = "Suckable";
		
		origScale = transform.localScale;
		origSize = size;
		origHealth = health;
		
		if(transform.parent.gameObject.GetComponent<GetSucked>() != null)
		{
			hasSuckyParent = true;
			canGetSucked = false;
			this.LockRigid();
		}
		
		foreach(Transform child in transform)
		{
			if(child.collider != null && this.collider != null)
			{
				Physics.IgnoreCollision(this.rigidbody.collider, child.rigidbody.collider);
			}
		}
	}
	
	public void SplitFromParent()
	{ //when damaged enough to the point where it is eliminated;
		if(hasSuckyParent == true)
		{
			transform.parent = null;
		}
		
		canGetSucked = true;
		this.UnLockRigid();
	}
	
	// Update is called once per physics frame
	void FixedUpdate ()
	{
		if(damage > 0) {	damage -= 0.2f;	}
		
		if(canGetSucked)
		{	
			Vector3 relVec = rigidbody.ClosestPointOnBounds(vacuumSucker.transform.position);
			relVec = relVec - vacuumSucker.transform.position;
			
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
	
	void LateUpdate()
	{

	}
	
	public void LockRigid()
	{
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		rigidbody.isKinematic = true;
	}
	
	public void UnLockRigid()
	{
		rigidbody.constraints = RigidbodyConstraints.None;
		rigidbody.isKinematic = false;
	}
	
	
	
	void OnGUI()
	{
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
