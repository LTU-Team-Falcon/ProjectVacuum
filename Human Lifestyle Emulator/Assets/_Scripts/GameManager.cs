using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour 
{
	public bool secondPhase = true;
	public float totalSceneWeight =0;
	public int objectCount =0;
	public GameObject DebugTextFab;
	
	public List<GameObject> inTheBag = new List<GameObject>();
	
	public bool isPaused = false;
	private bool canUnpause = true;
	
	// Use this for initialization
	void Start () 
	{
		findTotalSceneWeight();	
		//RenderSettings.ambientLight f;
		
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
					totalSceneWeight+= thisObject.rigidbody.mass;
					objectCount ++;
				}
			}
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
}