using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public float totalSceneWeight =0;
	public int objectCount =0;
	public GameObject DebugTextFab;
	
	// Use this for initialization
	void Start () 
	{
		findTotalSceneWeight();	
	}
	
	void findTotalSceneWeight()
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
	void Update () {
	
	}
}