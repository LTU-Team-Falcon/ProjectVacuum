﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class VacuumController : MonoBehaviour 
{
	public bool isOut = true;

	public GameObject playerObj;
	private GameObject camObj;
	
	private Vector3 downRotation;
	public Vector3 defaultLocPos;
	
	private Vector3 target;
	public VacuumSucker vacSucker;
	public VacuumPuncher vacPuncher;
	public VacuumShooter vacShooter;

	[HideInInspector]
	public bool isShootingPhase = false;
	
	// Use this for initialization
	void Start () 
	{
		playerObj = GameObject.FindGameObjectWithTag("Player");
		
		fixCollisions();
				
//		camObj = playerObj.transform.FindChild("Main Camera").gameObject;
		vacSucker = gameObject.GetComponentInChildren<VacuumSucker>();
		vacShooter = gameObject.GetComponentInChildren<VacuumShooter>();
		vacPuncher = gameObject.GetComponentInChildren<VacuumPuncher>();

		vacPuncher.vacController = this;
		vacSucker.vacController = this;
		downRotation = transform.localEulerAngles;
		defaultLocPos = transform.localPosition;

		print("init");
	}
	
	
	void fixCollisions()
	{
		List<Collider> childColliders = new List<Collider>();
		foreach(Transform child in transform)
		{
			if(child.collider != null)
			{
//				Physics.IgnoreCollision(playerObj.collider, child.collider);
				childColliders.Add(child.collider);
				//Physics.IgnoreCollision(this.collider, child.collider);
			}
		}
		
		foreach(Collider child1 in childColliders)
		{
			foreach(Collider child2 in childColliders)
			{
				if(child1 != child2)
				{
					Physics.IgnoreCollision(child1, child2);
				}
			}
		}
	}


	//Update Called OncePerPhysicsFrame
	void FixedUpdate() 
	{
		if(!isOut)
		{	//hold the vaccum downwards more;
			//transform.localEulerAngles = downRotation;
		}
		/*else if(vacPuncher.isPunching)
		{	//swing for the punch
			transform.position += transform.forward * (1.5f - transform.localPosition.z) * 0.5f;
		}*/
		else
		{	//if it is out and you arent punching; normal sucking deal
			//target = camObj.transform.position + camObj.transform.forward*2;
			//transform.LookAt(target);
		}
	}



	
	// Update is called once per frame
	void Update()
	{
		if(!isShootingPhase)
		{
			if(Input.GetMouseButtonUp(1))
			{
/*				vacSucker.isSucking = true;
				vacSucker.suckPow = vacSucker.suckPotential;
				isOut = true;
*/			}
			
			if(Input.GetMouseButton(1))
			{
/*				isOut = false;
				vacSucker.isSucking = false;
				vacSucker.dropIntake();
*/			}
			else
			{
				isOut = true;
			}
			
			if(Input.GetMouseButtonDown(0))
			{
				if(isOut && !vacPuncher.isPunching && vacPuncher.canPunchAgain)
				{
					//vacPuncher.initiatePunch();
				}
			}
		}
		else
		{
			if(Input.GetMouseButtonUp(1))
			{
				vacSucker.isSucking = true;
				vacSucker.suckPow = vacSucker.suckPotential;
				isOut = true;
			}
			
			if(Input.GetMouseButton(1))
			{
				isOut = false;
				vacSucker.isSucking = false;
				vacSucker.dropIntake();
			}
			else
			{
				isOut = true;
			}

			if(Input.GetMouseButton(0))
			{
				if(isOut && !vacPuncher.isPunching && vacPuncher.canPunchAgain)
				{
					vacPuncher.initiatePunch();
					vacShooter.ShootObject();
				}
			}
		}
	}
	
	
	void OnGUI()
	{
/*		string toGui = "SuckPow: " + vacSucker.suckPow;
		toGui += "\n SuckPotential: " + vacSucker.suckPotential;
		toGui += "\n isSucking: " + vacSucker.isSucking + "  isOut: " + isOut;
		GUI.Box (new Rect (10,50,200,100), toGui );*/
	}
}