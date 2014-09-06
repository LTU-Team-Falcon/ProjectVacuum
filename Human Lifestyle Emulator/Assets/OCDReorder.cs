using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class OCDReorder : MonoBehaviour 
{
	public int newIndex;
	public bool run = false;
	public int currentIndex;
	bool cleanUpAndRunAll = false;

	bool isCleaning = false;

	int Inum = 0;
	List<OCDReorder> allSiblingOrders = new List<OCDReorder>();

	// Use this for initialization
	void Start () 
	{
		currentIndex = transform.GetSiblingIndex() + 1;
		newIndex = currentIndex;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(isCleaning)
		{
			CleanAll();
		}

		currentIndex = transform.GetSiblingIndex() + 1;
		if(run)
		{
			run = false;
			transform.SetSiblingIndex(newIndex - 1);
		}

		if(cleanUpAndRunAll)
		{
		
			cleanUpAndRunAll = false;
			isCleaning = true;
			Inum = 0;
			if(transform.parent == null)
			{
				allSiblingOrders.Clear();
				allSiblingOrders.AddRange(GameObject.FindObjectsOfType<OCDReorder>());
			}
			else
			{
				allSiblingOrders.Clear();
				allSiblingOrders.AddRange(transform.parent.gameObject.GetComponentsInChildren<OCDReorder>());
			}
		}
	}

	void CleanAll()
	{
		int nextSmallest = 1000;
		bool hasrun = false;
		foreach(OCDReorder ocd in allSiblingOrders)
		{
			if(ocd.newIndex == Inum)
			{
				//print("clean baby");
				ocd.run = true;
				hasrun = true;
			}
			else if(ocd.newIndex > Inum && ocd.newIndex < nextSmallest)
			{
				nextSmallest = ocd.newIndex;
			}
		}

		Inum = nextSmallest;

		if(nextSmallest == 1000)
		{
			isCleaning = false;
		}
	}
}
