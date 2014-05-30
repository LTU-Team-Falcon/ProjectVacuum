using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetSucked : MonoBehaviour 
{
	[HideInInspector]
	public bool hasSuckyParent = false;
	
	public float mass = 2; //not implemented yet.

	[HideInInspector]
	public bool isShootingPhase = true;

	public bool isDebugging = false;
	TextMesh debugTextMesh;

	[HideInInspector]
	public bool canGetSucked = true;

	[HideInInspector]
	public Vector3 origScale;
	[HideInInspector]
	public float origSize;

	private VacuumSucker vacuumSucker;
	private Vector3 velocity;
	private Vector3 force;

	public float size = 0f;//once it has 0 health; this determines how fast it shrinks in the intake;



	[HideInInspector]
	public float health = 10f;

	public List<float> debugList = new List<float>();
	// Use this for initialization


	void Awake()
	{
		if(this.rigidbody == null)
		{
			gameObject.AddComponent<Rigidbody>(); 
			if(this.mass != 0)
			{
				this.rigidbody.mass = this.mass;
			}
		}
		
		if(this.collider == null)
		{
			gameObject.AddComponent<MeshCollider>();
		}
	}


	void Start () 
	{
		reCalcVar();

		vacuumSucker = GameObject.FindGameObjectWithTag("Vacuum").GetComponent("VacuumSucker") as VacuumSucker;
		gameObject.tag = "Suckable";

		origScale = transform.localScale;
		origSize = size;
	}
	
	public void reCalcVar()
	{
		if(!(size > 0)) size = rigidbody.mass;//temp 
	}
	
	// Update is called once per physics frame
	void FixedUpdate ()
	{//determines the force withwhich it is pulled towards the sucker.
		Vector3 force = new Vector3(0,0,0);
		Vector3 relVec = rigidbody.ClosestPointOnBounds(vacuumSucker.transform.position) - vacuumSucker.transform.position;
		
		float relDist = relVec.sqrMagnitude;
		
		if(relDist < vacuumSucker.suckDist2)
		{				
			force = relVec.normalized*vacuumSucker.suckPow;
			relDist = Mathf.Sqrt(relDist);
			force *= Mathf.Pow((vacuumSucker.suckDist - relDist), vacuumSucker.suckFalloff)/(Mathf.Pow (vacuumSucker.suckDist, vacuumSucker.suckFalloff));
			force *= Time.fixedDeltaTime*-60f;//multiplies it by the amount of time between each frame
			
			float AngleDot = Vector3.Dot(vacuumSucker.transform.forward, relVec);	
			AngleDot /= 3f;
			AngleDot = (Mathf.Clamp(AngleDot,-1,1f)+0.2f)/2f;
			AngleDot += 0.3f;
			force *= AngleDot; //AngleDot sets the "fulcrum" of the force
			
			rigidbody.AddForceAtPosition(force,relVec + vacuumSucker.transform.position);
		}
	}

	public void AddedToIntake()
	{
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		rigidbody.isKinematic = true;
		canGetSucked = false;
	}

	public void DroppedFromIntake()
	{
		rigidbody.constraints = RigidbodyConstraints.None;
		rigidbody.isKinematic = false;
		canGetSucked = true;

		transform.localScale = origScale;
		size = origSize;
		health = 10;

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
