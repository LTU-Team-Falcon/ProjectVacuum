using UnityEngine;
using System.Collections;

public class pauseUnpause : MonoBehaviour {
	XinputHandler inputHandler;

	public bool playerIsPaused = false;
	bool isFirstUpdate = true;
	float moveforce = 2000;
	float movejump = 4.7f;
	Vector3 Sensitivity = new Vector3(60f,60f,60f);

	GameObject PauseMenuOBJ;
	// Use this for initialization
	void Start () 
	{
		inputHandler = transform.parent.gameObject.GetComponent<XinputHandler>();


		PauseMenuOBJ = 	transform.parent.FindChild("HandCube").transform.FindChild("PauseMenuObj").gameObject;
		PauseMenuOBJ.GetComponent<PauseMenu>().pauseHandler = this;
		moveforce = gameObject.GetComponent<PhysicsFPSWalker>().force;
		movejump = gameObject.GetComponent<PhysicsFPSWalker>().jumpSpeed;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if(!isFirstUpdate)
		{
			if(inputHandler.GetButtonUp("Start") && Time.time > 1f)
			{
				if(playerIsPaused)
				{
					Unpause();
				}
				else
				{
					print("Yep");
					
					Pause();
				}
			}
		}
		else
		{
			isFirstUpdate = false;

		}
	}

	public void Pause()
	{
		playerIsPaused = true;
		PauseMenuOBJ.SetActive(true);

		moveforce = gameObject.GetComponent<PhysicsFPSWalker>().force;
		movejump = gameObject.GetComponent<PhysicsFPSWalker>().jumpSpeed;

		gameObject.GetComponent<PhysicsFPSWalker>().force = 0;
		gameObject.GetComponent<PhysicsFPSWalker>().jumpSpeed = 0;

		Rigidbody vacObj = transform.parent.FindChild("VacuumObject").GetComponent<Rigidbody>();
		vacObj.transform.localEulerAngles = Vector3.Scale(new Vector3(0,1,1), vacObj.transform.localEulerAngles);
		vacObj.constraints = RigidbodyConstraints.FreezeAll;

	}

	public void Unpause()
	{
		playerIsPaused = false;
		PauseMenuOBJ.SetActive(false);

		transform.parent.FindChild("HandCube").transform.FindChild("PauseMenuObj").gameObject.SetActive(false);

		gameObject.GetComponent<PhysicsFPSWalker>().force = moveforce;
		gameObject.GetComponent<PhysicsFPSWalker>().jumpSpeed = movejump;

		Rigidbody vacObj = transform.parent.FindChild("VacuumObject").GetComponent<Rigidbody>();
		vacObj.constraints = RigidbodyConstraints.None;

	}
}
