using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class VacuumSucker : MonoBehaviour 
{
	//[HideInInspector]
	//public float maxFlyVelocity = 20f;

	public float bagSize = 50f;

	public float suckPotential;
	public float maxSuckPotential;

	public float suckPow = 0f;
	public float suckFalloff = 0f;
	public float suckDist = 100f;
	[HideInInspector]
	public float suckDist2 = 1000f;

	[HideInInspector]
	public float massToSuction; // 3* (the difference between maxSuckPotenital and suckPotential) divided by the total number of objects in the scene
	[HideInInspector]
	public float countToSuction;
	[HideInInspector]
	public VacuumController vacController;

		
	[HideInInspector]
	public List<GameObject> intake = new List<GameObject>(0);
	[HideInInspector]
	public List<GameObject> suckQueue = new List<GameObject>(0);

	public GameObject actualSucker;
	
	
	// Use this for initialization
	void Start ()
	{
		suckDist2 = suckDist * suckDist;
		actualSucker = transform.GetChild(0).gameObject;

		actualSucker.transform.localScale = Vector3.Scale(transform.GetChild(0).localScale, new Vector3(1,1,suckDist));
		actualSucker.transform.localPosition = new Vector3(0,0,transform.GetChild(0).localScale.z*0.5f);

		vacController = transform.parent.gameObject.GetComponent<VacuumController>();

		GameManager gamemanage = GameObject.FindObjectOfType<GameManager> ();
/*		gamemanage.findTotalSceneWeight ();
		massToSuction = (float)((maxSuckPotential - suckPotential)/gamemanage.totalSceneWeight);
		countToSuction = (float)((maxSuckPotential - suckPotential)/(float)gamemanage.objectCount);
*/	}



	// Update is called once per physics frame
	void FixedUpdate () 
	{
		if(this.GetMass() > bagSize)
		{
			suckPow = 0;
			this.dropIntake();
		}
		if(this.suckPow > 0f)
		{
				foreach(GameObject i in intake)
				{ //shrinks objects stuck in intake and then eventually sucks them up completely
					GetSucked getSuckedI = i.GetComponent<GetSucked>();
					
					getSuckedI.health -= 1;
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
						{	//if the object is already way to small; we'll not shrink it at all
							getSuckedI.size = -1;
						}
					}
					
					if(getSuckedI.size < 0)
					{ //queue object to be actually sucked up officially at the lateUpdate
						suckQueue.Add(i);
					}
				}
				
				foreach(GameObject i in suckQueue)
				{ //removes objects from the intake that are queued to get sucked
					intake.Remove(i);
				}
		}
		else
		{
			//this.dropIntake();
		}
	}
	
	void LateUpdate()
	{
		foreach(GameObject i in suckQueue)
		{//Actually suck up the object and increase the intensity of the Vacuum sucker
			intake.Remove(i);
			i.SetActive(false);
			i.transform.parent = actualSucker.transform;
		}

		suckQueue.Clear();
	}

	
	void AddToIntake(GameObject parGameObj)
	{	//adds specified object to the stuck at intake list
		parGameObj.transform.parent = transform;

		parGameObj.GetComponent<GetSucked>().AddedToIntake();
		parGameObj.collider.enabled = false;
		intake.Add(parGameObj);
	}
	
	
	public void dropIntake()
	{ //drops and resets all objects currently stuck at the intake
		transform.DetachChildren();
		foreach(GameObject i in intake)
		{
			i.GetComponent<GetSucked>().DroppedFromIntake();
			i.collider.enabled = true;
		}
		intake.Clear();
		actualSucker.transform.parent = this.transform;
	}
	
	
	void OnTriggerEnter(Collider col)
	{ //checks if colliding object is ellidgable to get sucked into the vacuum and if it is, it adds it to the intake for further processing
		if(this.suckPow > 0f && (col.gameObject.tag == "Suckable" || col.gameObject.tag == "Bounce"))
		{	
				AddToIntake(col.gameObject);
		}
	}

	public float GetMass()
	{
		float sol =0;
		foreach(Transform suckedObj in actualSucker.transform)
		{
			sol += suckedObj.rigidbody.mass;
		}

		return sol;
	}

}
