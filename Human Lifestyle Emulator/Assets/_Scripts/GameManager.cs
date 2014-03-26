using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour 
{
	public bool secondPhase = false;
	public float totalSceneWeight =0;
	public int objectCount =0;
	public GameObject DebugTextFab;
	
	private float levelStartTime;
	public float firstPhaseTime =60f;
	public float secondPhaseTime = 60f;
	public List<GameObject> inTheBag = new List<GameObject>();
	
	// Use this for initialization
	void Start () 
	{
		levelStartTime = Time.time;
		findTotalSceneWeight();	
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
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI()
	{
		string sped = "Time: " + Mathf.Floor(Time.time - levelStartTime) + "  Score: " + inTheBag.Count;
		GUI.Box (new Rect (10,10,150,20), sped);
	}
}