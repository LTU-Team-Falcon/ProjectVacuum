using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour 
{
	public float totalSceneWeight =0;
	public int objectCount =0;
	public GameObject DebugTextFab;
	 
	public DisplayTime DisplayTime;

	public bool hasKeyboardPlayer = false;

	public bool isPaused = false;
	private bool canUnpause = true;

	private Score score;


	void Awake()
	{
		CheckForSettings();
	}


	// Use this for initialization
	void Start ()
	{
		findTotalSceneWeight();	
		score = GameObject.FindObjectOfType<Score>();
		//Physics.IgnoreCollision(vacController.transform.FindChild("Body").FindChild("VacuumObject").gameObject.collider, gameObject.collider);
	}
	
	public void findTotalSceneWeight()
	{
		totalSceneWeight = 0;
		objectCount = 0;
		GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
		foreach(GameObject thisObject in allObjects)
		{
			if (thisObject.activeInHierarchy)
			{
				if(thisObject.rigidbody != null)
				{
					totalSceneWeight += thisObject.rigidbody.mass;
					objectCount ++;
				}
			}
		}
	}

	public void shiftGameModes()
	{
		GetSucked[] suckers = GameObject.FindObjectsOfType<GetSucked>();
		foreach(GetSucked sucker in suckers)
		{
			HoloPositioner holo = sucker.gameObject.AddComponent<HoloPositioner>();

			holo.size = sucker.origSize;
			//holoList.Add(holo.gameObject);

			sucker.enabled = false;
		}
	}
	
	public GameObject createNewDebugTextObj()
	{
		return Instantiate(DebugTextFab) as GameObject;
	}
	
	public void Pause()
	{		
		isPaused = true;
		Screen.lockCursor = false;
		Screen.showCursor = true;
		RenderSettings.fog = true;//ambientLight *= 0.01f;

		PauseMenu[] pMenus = GameObject.FindObjectsOfType<PauseMenu>();

		foreach(PauseMenu pauseMenu in pMenus)
		{
			pauseMenu.gameObject.SetActive(true);
		}
	}
	
	public void ForcePause()
	{		
		isPaused = true;
		Screen.lockCursor = false;
		Screen.showCursor = true;
		RenderSettings.fog = true;//ambientLight *= 0.01f;

		PauseMenu[] pMenus = GameObject.FindObjectsOfType<PauseMenu>();

		foreach(PauseMenu pauseMenu in pMenus)
		{
			pauseMenu.gameObject.SetActive(true);
			pauseMenu.transform.FindChild("MenuContinue").gameObject.SetActive(false);
			pauseMenu.transform.FindChild("MenuReload").gameObject.SetActive(false);
		}
		canUnpause = false;
		 //SetActive(false);
		
	}
	
	public void UnPause()
	{
		RenderSettings.fog = false;
		
		isPaused = false;
		
		Screen.lockCursor = true;
		Screen.showCursor = false;
		
	//	transform.Find("PauseMenuObj").gameObject.SetActive(false); //SetActive(false);
		PauseMenu[] pMenus = GameObject.FindObjectsOfType<PauseMenu>();

		foreach(PauseMenu pauseMenu in pMenus)
		{
			pauseMenu.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame

	void Update ()
	{
		if (DisplayTime.timeLeft < 1) 
		{
			GameObject GUICamera = GameObject.Find("InGameCamera");
			GUICamera.camera.enabled = false;
			GameObject EndGameCamera = GameObject.Find("EndGameCamera");
			EndGameCamera.camera.enabled = true;
			Screen.lockCursor = false;
			Screen.showCursor = true;
		}
	}



	public int getNumberOfPlayers()
	{
		int sol = Input.GetJoystickNames().Length;
		if(hasKeyboardPlayer && sol < 4)
		{
			sol += 1;
		}
		else if(hasKeyboardPlayer && sol == 4)
		{
			hasKeyboardPlayer = false;
		}
		return sol;
	}


	/*public void StartBeenPressed() 
	{
		if(!isPaused)
		{
			Pause();
		}
		else if(canUnpause)
		{
			UnPause();
		}
	}*/
	
	void CheckForSettings()
	{
		SettingsData settingsdata = Object.FindObjectOfType<SettingsData>();
		if(settingsdata != null)
		{
			this.hasKeyboardPlayer = settingsdata._HasKeyboard;
		}
	}
}