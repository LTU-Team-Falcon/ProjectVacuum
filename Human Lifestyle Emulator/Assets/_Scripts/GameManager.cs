using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour 
{
	public float totalSceneWeight =0;
	public int objectCount =0;
	public GameObject DebugTextFab;
	public float Damage;


	public List<GameObject> holoList = new List<GameObject>();
	
	public List<GameObject> inTheBag = new List<GameObject>();

	public bool isPaused = false;
	private bool canUnpause = true;

	private Score score;


	// Use this for initialization
	void Start ()
	{
		findTotalSceneWeight();	
		score = GameObject.FindObjectOfType<Score>();
		//Physics.IgnoreCollision(vacController.transform.FindChild("Body").FindChild("VacuumObject").gameObject.collider, gameObject.collider);
		Damage = 0;
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

		GameObject[] pauses = GameObject.FindGameObjectsWithTag("Menu");
		
		foreach(GameObject pauseMenu in pauses)
		{
			pauseMenu.SetActive(true);
		}
	}
	
	public void ForcePause()
	{		
		isPaused = true;
		Screen.lockCursor = false;
		Screen.showCursor = true;
		RenderSettings.fog = true;//ambientLight *= 0.01f;

		GameObject[] pauses = GameObject.FindGameObjectsWithTag("Menu");

		foreach(GameObject pauseMenu in pauses)
		{
			pauseMenu.SetActive(true);
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
		GameObject[] pauses = GameObject.FindGameObjectsWithTag("Menu");

		foreach(GameObject pauseMenu in pauses)
		{
			pauseMenu.SetActive(false);
		}
	}
	
	// Update is called once per frame


	public void StartBeenPressed() 
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


	void OnCollisionEnter (Collision col)
	{
/*		if (col.gameObject.tag == "Fired")
		{
			Vector3 ConPoint = col.contacts[0].point;
			float power = col.rigidbody.mass * 5000 + (Damage * 100);

			this.rigidbody.AddExplosionForce(power, col.transform.position, 1f, 0f);


			print("Added force of " + power + " to " + this.gameObject);

			Damage += col.rigidbody.mass * 1.5f;

			col.gameObject.tag = "Suckable";
		}
*/
	}
}