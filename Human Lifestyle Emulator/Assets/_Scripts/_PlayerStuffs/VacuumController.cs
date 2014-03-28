using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class VacuumController : MonoBehaviour 
{

	public bool isOut = true;
	private float punchLength = 0.2f;
	private float punchCoolDown = 0.2f;
	public bool canPunchAgain = true;
	public bool isPunching = false;
	
	public GameObject playerObj;
	private GameObject camObj;
	
	private Vector3 downRotation;
	private Vector3 defaultLocPos;
	
	private Vector3 target;
	public VacuumSucker vacSucker;
	public VacuumPuncher vacPuncher;
	
	// Use this for initialization
	void Start () 
	{
		playerObj = GameObject.FindGameObjectWithTag("Player");
		
		fixCollisions();
				
		camObj = playerObj.transform.FindChild("Main Camera").gameObject;
		vacSucker = gameObject.GetComponentInChildren<VacuumSucker>();
		vacPuncher = gameObject.GetComponentInChildren<VacuumPuncher>();
		vacSucker.vacController = this;
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
				Physics.IgnoreCollision(playerObj.collider, child.collider);
				childColliders.Add(child.collider);
			}
		}
		
		foreach(Collider child1 in childColliders)
		{
			foreach(Collider child2 in childColliders)
			{
				if(child1 != child2)
				{
//					Physics.IgnoreCollision(child1, child2);
				}
			}
		}
	}
	//Update Called OncePerPhysicsFrame
	void FixedUpdate() 
	{
		if(!isOut)
		{	//hold the vaccum downwards more;
			transform.localEulerAngles = downRotation;
		}
		else if(isPunching)
		{	//swing for the punch
			transform.position += transform.forward * (1.5f - transform.localPosition.z) * 0.5f;
		}
		else
		{	//if it is out and you arent punching; normal sucking deal
			target = camObj.transform.position + camObj.transform.forward*2;
			transform.LookAt(target);
		}
	}
	

	
	void DonePunching()
	{
		rigidbody.AddExplosionForce(vacSucker.suckPow/2f, transform.position, 1.5f);
		isPunching = false;
		transform.localPosition = defaultLocPos;
		Invoke("EnablePunch", punchCoolDown);
		//vacSucker.isSucking = false;
	}
	
	void EnablePunch()
	{
		canPunchAgain = true;
		//vacSucker.isSucking = true;
	}
	
	// Update is called once per frame
	void Update()
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
		
		if(Input.GetMouseButtonDown(0))
		{
			if(isOut && !isPunching && canPunchAgain)
			{
				canPunchAgain = false;
				isPunching = true;
				Invoke("DonePunching", punchLength);
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