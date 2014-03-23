using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public float totalSceneWeight;
	public GameObject DebugTextFab;
	
	// Use this for initialization
	void Start () 
	{
		findTotalSceneWeight();	
	}
	
	void findTotalSceneWeight()
	{
		totalSceneWeight = 0;
		GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
		foreach(GameObject thisObject in allObjects)
		{
			if (thisObject.activeInHierarchy)
			{
				if(thisObject.rigidbody != null)
				{
					totalSceneWeight+= thisObject.rigidbody.mass;
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