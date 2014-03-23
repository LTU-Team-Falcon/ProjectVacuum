using UnityEngine;
using System.Collections;

public class VacuumController : MonoBehaviour 
{


	public bool isOut;
	private GameObject playerObj;
	private GameObject camObj;
	
	private Vector3 defaultRot;
	
	private Vector3 target;
	private VacuumSucker vacSucker;
	
	// Use this for initialization
	void Start () 
	{
		playerObj = GameObject.FindGameObjectWithTag("Player");
		camObj = playerObj.transform.FindChild("Main Camera").gameObject;
		vacSucker = transform.GetComponentInChildren<VacuumSucker>();
		vacSucker.vacController = this;
		defaultRot = transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(isOut)
		{
			target = camObj.transform.position + camObj.transform.forward*2;
			transform.LookAt(target);
		}
		else
		{
			transform.localEulerAngles = defaultRot;
		}
	}
	
	void Update()
	{
		if(Input.GetMouseButtonUp(0))
		{
			vacSucker.isSucking = true;
		}
		
		if(Input.GetMouseButton(0))
		{
			isOut = false;
			vacSucker.isSucking = false;
			vacSucker.dropIntake();
		}
		else
		{
			isOut = true;
		}
	}
}