using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class VacuumController : MonoBehaviour 
{
	public float shotPower = 40f;
	public int rateOfFire = 15;
	private int shotTime = 0;
	public int bulletsPerShot = 1;

	public float spread = 10f;
	public bool isOut = true;

	public GameObject playerObj;
	private GameObject camObj;
	
	private Vector3 downRotation;
	public Vector3 defaultLocPos;
	
	private Vector3 target;
	public VacuumSucker vacSucker;
	//public VacuumPuncher vacPuncher;
	public VacuumShooter vacShooter;

	public Transform BlowingParticles;
	public Transform SuckingParticles;

	[HideInInspector]
	public XinputHandler control;
	GameManager gameManager;
	public bool IsSucking;

	public AudioClip Shooting;
	public AudioClip Sucking;

	bool looping = false;
	int looptime;

	// Use this for initialization
	void Start () 
	{
		IsSucking = true;
		gameManager = GameObject.FindObjectOfType<GameManager>();

		playerObj = transform.parent.gameObject;
		if(transform.parent.gameObject.GetComponent<XinputHandler>() != null)
		{
			control = transform.parent.gameObject.GetComponent<XinputHandler>();
		}

		BlowingParticles = transform.Find ("Blowing Out Particles Burst");
		SuckingParticles = transform.Find ("Sucking in Particles");
		SuckingParticles.particleSystem.Stop ();
		//fixCollisions();
				
//		camObj = playerObj.transform.FindChild("Main Camera").gameObject;
		vacSucker = gameObject.GetComponentInChildren<VacuumSucker>();
		vacShooter = gameObject.GetComponentInChildren<VacuumShooter>();
/*		vacPuncher = gameObject.GetComponentInChildren<VacuumPuncher>();

		//vacPuncher.vacController = this;
*/		vacSucker.vacController = this;
		downRotation = transform.localEulerAngles;
		defaultLocPos = transform.localPosition;

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

	// Update is called once per frame
	void Update()
	{
		shotTime --;

		/*if(control.GetButtonDown("Start"))
		{
			gameManager.StartBeenPressed();
		}*/

		if(control.GetRightTrigger() > 0.7f && shotTime <= 0)
		{
			vacSucker.suckPow = 0;
			for(int i = 1; i <= bulletsPerShot; i++)
			{
				ShootObjects( shotPower ); 
			}

			shotTime = rateOfFire;
		}
		else if(control.GetLeftTrigger() > 0f)
		{
			vacSucker.suckPow = control.GetLeftTrigger() * vacSucker.suckPotential;
			control.SetVibration(new Vector2(0, control.GetLeftTrigger()*0.4f ));

			if (audio.clip != Sucking)
			{
				audio.loop = true;
				audio.clip = Sucking;
				audio.Play();
				SuckingParticles.particleSystem.Play();
			}
		}
		else if(control.GetLastLeftTrigger() > 0f && control.GetLeftTrigger() <= 0f)
		{
			vacSucker.suckPow = control.GetLeftTrigger() * vacSucker.suckPotential;

			Invoke("HaltVibrations", 0.2f);
			audio.Stop();
			audio.clip = null;
			SuckingParticles.particleSystem.Stop();
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
			audio.loop = false;
			audio.clip = Shooting;
			audio.Play();
			BlowingParticles.particleSystem.Play();
			Transform projectile = vacSucker.actualSucker.transform.GetChild(0);
			projectile.tag = "Fired";
			projectile.parent = null;

			projectile.transform.position += vacSucker.transform.forward;
			projectile.tag = "Fired";
			projectile.gameObject.SetActive(true);
			projectile.collider.enabled = true;


			projectile.GetComponent<GetSucked>().DroppedFromIntake();
			projectile.GetComponent<GetSucked>().DealWithColliders(transform.parent.FindChild("Player").gameObject);


			projectile.rigidbody.velocity = vacSucker.transform.forward * parPower;
		}
	}
	

	void HaltVibrations()
	{
		control.SetVibration(new Vector2(0,0));
		//stop particles
		BlowingParticles.particleSystem.Stop ();
	}
	

	void OnGUI()
	{
/*		string toGui = "SuckPow: " + vacSucker.suckPow;
		toGui += "\n SuckPotential: " + vacSucker.suckPotential;
		toGui += "\n isSucking: " + vacSucker.isSucking + "  isOut: " + isOut;
		GUI.Box (new Rect (10,50,200,100), toGui );*/
	}
}