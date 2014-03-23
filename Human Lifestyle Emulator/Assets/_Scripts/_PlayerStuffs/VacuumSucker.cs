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
	[HideInInspector]
	public float countToSuction;
	[HideInInspector]
	public VacuumController vacController;
	
	
	public List<GameObject> intake = new List<GameObject>(0);
	public List<GameObject> hasSucked = new List<GameObject>(0);// Learn how to serialize bro
	
	
	public GameObject playerObj;
	// Use this for initialization
	void Start ()
	{
		suckDist2 = suckDist*suckDist;
		playerObj = GameObject.FindGameObjectWithTag("Player");
		massToSuction = (float)((maxSuckPower - suckPotential)/playerObj.GetComponentInChildren<GameManager>().totalSceneWeight);
		countToSuction = (float)((maxSuckPower - suckPotential)/(float)playerObj.GetComponentInChildren<GameManager>().objectCount);
	}
	
	// Update is called once per frame
	void Update () 
	{
		suckPow = suckPotential;
		
		if(isSucking)
		{
				foreach(GameObject i in intake)
				{
					if(i.GetComponent<GetSucked>().resistance > suckPow*3)
					{
						suckPow = 0; 
						this.isSucking = false;
						return;
					}
				}

				
				foreach(GameObject i in intake)
				{
					
					i.GetComponent<GetSucked>().health -= (float)(suckPow / 8f);
					i.transform.localEulerAngles += new Vector3(10f*Mathf.Cos(Time.time), 10f*Mathf.Sin(Time.time), 10f*Mathf.Cos(Time.time + 53));
					
					if(i.GetComponent<GetSucked>().health <= 0)
					{
						if(i.transform.localScale.sqrMagnitude > 0.001f)
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
			suckPotential += countToSuction;
			Destroy(i);
		}
		hasSucked.Clear();
		
		if(!isSucking && vacController.isOut)
		{
			if(!transform.parent.GetComponentInChildren<ParticleSystem>().isPlaying)
			{
				transform.parent.GetComponentInChildren<ParticleSystem>().Play();
			}
		}
	}
	
	void AddToIntake(GameObject parGameObj)
	{
		parGameObj.transform.parent = transform;
		parGameObj.GetComponent<GetSucked>().LockRigid();
		parGameObj.GetComponent<GetSucked>().canGetSucked = false;
		parGameObj.collider.enabled = false;
		intake.Add(parGameObj);
	}
	
	public void dropIntake()
	{
		transform.DetachChildren();
		foreach(GameObject i in intake)
		{
			i.transform.localScale = i.GetComponent<GetSucked>().origSize;
			i.GetComponent<GetSucked>().UnLockRigid();
			i.GetComponent<GetSucked>().canGetSucked = true;
			i.collider.enabled = true;
		}
		intake.Clear();
	}
	
	
	void OnTriggerEnter(Collider col)
	{
		if(this.isSucking && col.gameObject.tag == "Suckable")
		{
			if(col.gameObject.GetComponent<GetSucked>().canGetSucked && (suckPow >= col.gameObject.GetComponent<GetSucked>().resistance))
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
