using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class VacuumSucker : MonoBehaviour 
{
	public float maxFlyVelocity = 20f;
	
	
	public float suckPotential;
	public float maxSuckPower;
	
	public float maxSuckDist;
	public float suckDist;
	[HideInInspector]
	public float suckDist2;
	[HideInInspector]
	public bool isSucking = true;
	[HideInInspector]
	public float suckPow;
	[HideInInspector]
	public float massToSuction;
	
	public List<GameObject> intake = new List<GameObject>(0);
	public List<GameObject> hasSucked = new List<GameObject>(0);// Learn how to serialize bro
	
	
	public GameObject playerObj;
	// Use this for initialization
	void Start ()
	{
		suckDist2 = suckDist*suckDist;
		playerObj = GameObject.FindGameObjectWithTag("Player");
		massToSuction = (float)((maxSuckPower - suckPotential)/playerObj.GetComponentInChildren<GameManager>().totalSceneWeight);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		suckPow = suckPotential;
		
		if(isSucking)
		{
				
				foreach(GameObject i in intake)
				{
					if(i.GetComponent<GetSucked>().resistance > suckPow)
					{
						suckPow -= (i.GetComponent<GetSucked>().resistance - suckPow)*0.5f;
					}
				}
				
				if(suckPow < 0)
				{
					suckPow = 0; 
					this.isSucking = false;
					return;
				}
				
				foreach(GameObject i in intake)
				{
					
					i.GetComponent<GetSucked>().health -= (float)(suckPow / 8f);
						
					if(i.GetComponent<GetSucked>().health <= 0)
					{
						if(i.transform.localScale.sqrMagnitude > 0.05f)
						{
							i.transform.localScale *= 0.7f;
							i.transform.localPosition *= 0.7f;
						}
						else
						{
							i.GetComponent<GetSucked>().health = -21;
						}
					}
					
					if(i.GetComponent<GetSucked>().health < -20)
					{
						hasSucked.Add(i); 
					}
				}
		}
		else
		{
			suckPow = 0;
		}
	}
	
	void LateUpdate()
	{
		foreach(GameObject i in hasSucked)
		{
			intake.Remove(i);
			suckPotential += (float)i.rigidbody.mass * massToSuction;
			Destroy(i);
		}
		hasSucked.Clear();
	}
	
	void AddToIntake(GameObject parGameObj)
	{
		parGameObj.transform.parent = transform;
		parGameObj.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		parGameObj.rigidbody.isKinematic = true;
		parGameObj.GetComponent<GetSucked>().canGetSucked = false;
		parGameObj.collider.enabled = false;
		intake.Add(parGameObj);
	}
	
	public void dropIntake()
	{
		transform.DetachChildren();
		foreach(GameObject i in intake)
		{
			i.rigidbody.constraints = RigidbodyConstraints.None;
			i.rigidbody.isKinematic = false;
			i.GetComponent<GetSucked>().canGetSucked = true;
			i.collider.enabled = true;
		}
		intake.Clear();
	}
	
	
	void OnTriggerEnter(Collider col)
	{
		if(this.isSucking && col.gameObject.tag == "Suckable")
		{
			if(col.gameObject.GetComponent<GetSucked>().canGetSucked)
			{
				AddToIntake(col.gameObject);
			}
		}
	}
	
	
	void OnGUI()
	{
		string toGui = "SuckPow: " + suckPow;
		toGui += "\n SuckPotential: " + suckPotential;
		toGui += "\n isSucking: " + isSucking; 
		GUI.Box (new Rect (10,50,150,70), toGui );
	}
}
