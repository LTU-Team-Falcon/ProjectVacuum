using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class VacuumSucker : MonoBehaviour 
{
	//[HideInInspector]
	//public float maxFlyVelocity = 20f;
	[HideInInspector]
	public GameObject playerObj;
	
	
	public float suckPotential;
	public float maxSuckPotential;
	
	public float maxSuckDist;
	
	public float suckDist;
	[HideInInspector]
	public float suckDist2;
	[HideInInspector]
	public bool isSucking = true;
	[HideInInspector]
	public float suckPow; //power with witch the vaccum is sucking right now
	[HideInInspector]
	public float massToSuction; // 4* (the difference between maxSuckPotenital and suckPotential) divided by the total number of objects in the scene
	[HideInInspector]
	public float countToSuction;
	[HideInInspector]
	public VacuumController vacController;

		
	[HideInInspector]
	public List<GameObject> intake = new List<GameObject>(0);
	[HideInInspector]
	public List<GameObject> hasSucked = new List<GameObject>(0);// Learn how to serialize bro
	
	
	
	
	// Use this for initialization
	void Start ()
	{
		suckDist2 = suckDist*suckDist;
		vacController = transform.parent.gameObject.GetComponent<VacuumController>();
		this.playerObj = GameObject.FindGameObjectWithTag("Player");
		vacController.playerObj = this.playerObj;
		playerObj.GetComponentInChildren<GameManager>().findTotalSceneWeight();
		massToSuction = 4f*(float)((maxSuckPotential - suckPotential)/playerObj.GetComponentInChildren<GameManager>().totalSceneWeight);
		countToSuction = (float)((maxSuckPotential - suckPotential)/(float)playerObj.GetComponentInChildren<GameManager>().objectCount);
		
	}
	
	// Update is called once per physics frame
	void FixedUpdate () 
	{
	
		//print ("suckPow" + suckPow + " suckPot" + suckPotential);
		suckPow = suckPotential;
		
		if(isSucking)
		{
				foreach(GameObject i in intake)
				{ //reduces the power of the sucking dependent on th esize and number of whatever objects are stucked
					suckPow = suckPow/i.GetComponent<GetSucked>().resistance; 				
				}
				
			
				if(suckPow <= 0.2f)
				{//#SPARKS checks to see if the objects in the intake are have plugged up the intake enough to "short circuite it" and shoot sparks
									
					this.isSucking = false;
					vacController.vacPuncher.gameObject.GetComponent<ParticleSystem>().Play();
				
					return;	
				}
				
				foreach(GameObject i in intake)
				{//shrinks objects stuck in intake and then eventually sucks them up completely
					GetSucked getSuckedI = i.GetComponent<GetSucked>();
					
					getSuckedI.health -= (float)(suckPow/16f);
					i.transform.localEulerAngles += new Vector3(3*Mathf.Cos(Time.time*45f), 0, 0);//adds the jittering affect when objects are caught up in the intake
					if(getSuckedI.health <= 0)
					{
						if(i.transform.localScale.sqrMagnitude > 0.001f)
						{ //if the object isn't super small already shrink it a little bit
							i.transform.localScale *= 0.7f;
							i.transform.localPosition *= 0.7f;
							
							getSuckedI.size -= 1;
						}
						else
						{
							getSuckedI.size = -1;
						}
					}
					
					if(getSuckedI.size < 0)
					{ //queue object to be actually sucked up officially at the lateUpdate
						hasSucked.Add(i);
					}
				}
				
				foreach(GameObject i in hasSucked)
				{///removes objects from the intake that are queued to get sucked
					intake.Remove(i);
				}
		}
		else
		{
			suckPow = 0;
		}
		
		//print ("FixedUpdate: " + suckPotential);
	}
	
	void LateUpdate()
	{
		foreach(GameObject i in hasSucked)
		{//Actually suck up the object and increase the intensity of the Vacuum sucker
			//intake.Remove(i);
			suckPotential += i.rigidbody.mass * i.rigidbody.mass * massToSuction * massToSuction;
			playerObj.GetComponent<GameManager>().inTheBag.Add(i);
			i.SetActive(false);
			//Destroy(i);
		}
		hasSucked.Clear();
		
		if(suckPow <= (suckPotential/2) && vacController.isOut)
		{
			if(!transform.parent.FindChild("Body").GetComponentInChildren<ParticleSystem>().isPlaying)
			{//#SMOKES adds smoke if it be workin hard to suck something down
				transform.parent.GetComponentInChildren<ParticleSystem>().Play();
			}
		}
		
		//print ("lateUpdate: " + suckPotential);
	}
	
	void AddToIntake(GameObject parGameObj)
	{	//adds specified object to the stuck at intake list
		parGameObj.transform.parent = transform;
		parGameObj.GetComponent<GetSucked>().LockRigid();
		parGameObj.GetComponent<GetSucked>().canGetSucked = false;
		parGameObj.collider.enabled = false;
		intake.Add(parGameObj);
	}
	
	public void dropIntake()
	{ //drops and resets all objects currently stuck at the intake
		transform.DetachChildren();
		foreach(GameObject i in intake)
		{
			GetSucked j = i.GetComponent<GetSucked>();
			i.transform.localScale = j.origScale;
			j.UnLockRigid();
			j.canGetSucked = true;
			j.size = j.origSize;
			j.health = j.origHealth;
			i.collider.enabled = true;
		}
		intake.Clear();
	}
	
	
	void OnTriggerEnter(Collider col)
	{ //checks if colliding object is ellidgable to get sucked into the vacuum and if it is, it adds it to the intake for further processing
		if(this.isSucking && col.gameObject.tag == "Suckable" && (col.rigidbody.velocity.sqrMagnitude > 0.5f || col.gameObject.GetComponent<GetSucked>().size == 1 ))
		{	//if the vacuum is sucking AND the colliding object is suckable AND EITHER the object is moving, or the object is super small; might remove that last part
			if(col.gameObject.GetComponent<GetSucked>().canGetSucked)
			{
				AddToIntake(col.gameObject);
			}
		}
	}
	
	

}
