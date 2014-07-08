using UnityEngine;
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

	XinputHandler control;
	GameManager gameManager;

	// Use this for initialization
	void Start () 
	{
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

		playerObj = transform.parent.gameObject;
		if(transform.parent.gameObject.GetComponent<XinputHandler>() != null)
		{
			control = transform.parent.gameObject.GetComponent<XinputHandler>();
		}
		else
		{
			control = (XinputHandler)transform.parent.gameObject.GetComponent<XinputKeyboard>();
		}

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
		if(control.GetButtonDown("Start"))
		{
			gameManager.StartBeenPressed();
		}

		if(control.GetRightTrigger() > 0.7f)
		{
			vacSucker.suckPow = 0;

			ShootObjects( 50f ); 

		}
		else if(control.GetLeftTrigger() > 0f)
		{
			vacSucker.suckPow = control.GetLeftTrigger() * vacSucker.suckPotential;
			control.SetVibration(new Vector2(0, control.GetLeftTrigger()*0.4f ));
		}
		else if((control.GetLastLeftTrigger() > 0f && control.GetLeftTrigger() <= 0f) )
		{
			Invoke("HaltVibrations", 1f);
		}
		else
		{
			vacSucker.suckPow = 0;
		}
	}


	void ShootObjects(float parPower)
	{
		int children = vacSucker.actualSucker.transform.childCount;

		if(children >0)
		{
			control.SetVibration(new Vector2(1,0.5f));
			Invoke("HaltVibrations", 0.33f);

			Transform projectile = vacSucker.actualSucker.transform.GetChild(0);
			projectile.tag = "Fired";
			projectile.parent = null;

			projectile.transform.position += vacSucker.transform.forward;
			projectile.tag = "Fired";
			projectile.gameObject.SetActive(true);
			projectile.collider.enabled = true;

			projectile.GetComponent<GetSucked>().DroppedFromIntake();

			projectile.rigidbody.velocity = vacSucker.transform.forward * parPower;
		}

	}

	void HaltVibrations()
	{
		control.SetVibration(new Vector2(0,0));
	}
	void OnGUI()
	{
/*		string toGui = "SuckPow: " + vacSucker.suckPow;
		toGui += "\n SuckPotential: " + vacSucker.suckPotential;
		toGui += "\n isSucking: " + vacSucker.isSucking + "  isOut: " + isOut;
		GUI.Box (new Rect (10,50,200,100), toGui );*/
	}
}