using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetSucked : MonoBehaviour 
{
	float minVelocity = 1f;

	public bool isBox = false;
	[HideInInspector]
	public bool hasSuckyParent = false;
	
	public float mass = 2; //not implemented yet.

	[HideInInspector]
	public bool isShootingPhase = true;

	public bool isDebugging = false;
	TextMesh debugTextMesh;

	[HideInInspector]
	public bool canGetSucked = true;

	

	public Vector3 origScale;

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
			if(isBox)
			{
				gameObject.AddComponent<BoxCollider>();
			}
			else
			{
				gameObject.AddComponent<MeshCollider>();
			}

			if(this.collider == null)
			{
				GameObject gammy = (GameObject)Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube),this.transform.position,this.transform.rotation);
				gammy.transform.localScale *= 10f;
				gammy.renderer.material = null;
				Destroy(this.gameObject);
			}
		}
	}


	void Start () 
	{
		reCalcVar();

		gameObject.tag = "Suckable";

		origScale = transform.localScale;
		origSize = size;
	}

	GameObject lastshotfrom;
	public void DealWithColliders(GameObject parGamObj)
	{
		lastshotfrom = parGamObj;

		Physics.IgnoreCollision(lastshotfrom.collider , gameObject.collider, true);

/*		Collider[] colliders = lastshotfrom.GetComponentsInChildren<Collider>();
		foreach(Collider col in colliders)
		{
			Physics.IgnoreCollision(col , gameObject.collider, true);
		}*/

		Invoke("UnDealWithColliders", 1f);
	}

	void UnDealWithColliders()
	{
		Physics.IgnoreCollision(lastshotfrom.collider , gameObject.collider, false);


	/*	Collider[] colliders = lastshotfrom.GetComponentsInChildren<Collider>();
		foreach(Collider col in colliders)
		{
			Physics.IgnoreCollision(col , gameObject.collider, false);
		}*/
	}
	
	public void reCalcVar()
	{
		if(!(size > 0)) size = rigidbody.mass;//temp 
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

			

		transform.localScale = origScale*25f;
		size = origSize;
		health = 10;

	}


	void OnCollisionEnter(Collision col) 
	{
		if (this.gameObject.tag == "Fired" && col.gameObject.tag == "DestuctableWalls") 
		{
			Vector3 direction =  transform.position - col.transform.position;
			rigidbody.AddForce(direction.normalized * 150);


			if(col.gameObject.GetComponent<Rigidbody>() == null)
			{
				col.gameObject.AddComponent<Rigidbody>();
				col.gameObject.GetComponent<Rigidbody>().useGravity = true;
				col.gameObject.GetComponent<DestructibleWalls>().SelfDestruct();

			}

		}
	}
	


	void Update()
	{
		if(this.tag == "Fired")
		{
			if(rigidbody.velocity.magnitude < minVelocity)
			{
				this.tag = "Suckable";
			}
		}
	}



	
	
	void OnGUI()
	{//this is all stuff for debugging; right now its set to display text of the current force on the object; can be changed to whatever pretty easily though
		if(isDebugging)
		{
			if(this.isDebugging && debugTextMesh == null)
			{
				GameObject textObj = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().createNewDebugTextObj();
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
