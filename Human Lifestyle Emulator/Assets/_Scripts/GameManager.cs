using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour 
{
	public bool isShootingPhase = true;
	public float totalSceneWeight =0;
	public int objectCount =0;
	public GameObject DebugTextFab;

	public List<GameObject> holoList = new List<GameObject>();
	
	public List<GameObject> inTheBag = new List<GameObject>();

	public bool isPaused = false;
	private bool canUnpause = true;

	private VacuumSucker vacSucker;
	private VacuumShooter vacShooter;
	private VacuumController vacController;
	private Score score;

	// Use this for initialization
	void Start ()
	{
		findTotalSceneWeight();	
		vacController = GameObject.FindObjectOfType<VacuumController>();
		vacController.isShootingPhase = isShootingPhase;
		vacSucker = GameObject.FindObjectOfType<VacuumSucker>();
		vacShooter = GameObject.FindObjectOfType<VacuumShooter>();
		score = GameObject.FindObjectOfType<Score>();
		Physics.IgnoreCollision(vacController.transform.FindChild("Body").FindChild("VacuumObject").gameObject.collider, gameObject.collider);
		if(isShootingPhase)
		{
			shiftGameModes();
		}

		vacShooter.gameManager = this;
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
			holoList.Add(holo.gameObject);

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
		
		transform.Find("PauseMenuObj").gameObject.SetActive(true); //SetActive(false);
		
	}
	
	public void ForcePause()
	{		
		isPaused = true;
		Screen.lockCursor = false;
		Screen.showCursor = true;
		RenderSettings.fog = true;//ambientLight *= 0.01f;
		
		GameObject pauseMenu = transform.Find("PauseMenuObj").gameObject;
		pauseMenu.SetActive(true);
		pauseMenu.transform.FindChild("MenuContinue").gameObject.SetActive(false);
		pauseMenu.transform.FindChild("MenuReload").gameObject.SetActive(false);
		
		canUnpause = false;
		 //SetActive(false);
		
	}
	
	
	
	public void UnPause()
	{
		RenderSettings.fog = false;
		
		isPaused = false;
		
		Screen.lockCursor = true;
		Screen.showCursor = false;
		
		transform.Find("PauseMenuObj").gameObject.SetActive(false); //SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Pause") == true)
		{
			if(!isPaused)
			{
				Pause();
			}
			else if(canUnpause)
			{
				UnPause();
			}
		}
	}

	public void EndSucking()
	{
		vacSucker.isSucking = false;
		score.transform.localPosition = new Vector3(score.transform.localPosition.x,0,score.transform.localPosition.z);
		score.texty.characterSize = 0.3f;
		score.texty.text = "Score : " + score.playerScore;
		SaveSuckedObjects();
		Application.LoadLevel(2);
	}

	public void EndShooting()
	{
		Application.LoadLevel(0);
	}

	void SaveSuckedObjects()
	{
		int size = inTheBag.Count;
		PlayerPrefs.SetInt("totalSucked", size);
		PlayerPrefs.SetInt("Score", score.playerScore);

		
		for(int i = 0; i< size; i++)
		{
			int j = size - 1 - i;
			string ID = "sucked" + j.ToString();
			PlayerPrefs.SetString(ID, inTheBag[j].name);
		}
	}
}