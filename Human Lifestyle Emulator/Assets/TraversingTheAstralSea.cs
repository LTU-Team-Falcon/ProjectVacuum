using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TraversingTheAstralSea : MonoBehaviour 
{
	public List<GameObject> systems = new List<GameObject>();
	public GameObject warpSystem;

	float tim;

	public float warpTimeModifier = 10f;

	bool isInWarp = true;
	public int selector = 0;

	//public float currentLocation = 100;
	// Use this for initialization
	void Start () 
	{
		foreach(Transform child in transform)
		{
			if(child.name == "WarpSystem")
			{
				warpSystem = child.gameObject;
			}
			else
			{
				systems.Add(child.gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		tim = Mathf.Cos(Time.time/warpTimeModifier);

		if(tim < 0 && isInWarp)
		{
			warpSystem.GetComponent<CelestialSystem>().OnWarpOut();

			warpSystem.SetActive(false);
			selector = (int)(Random.value * systems.Count);
			isInWarp = false;
			systems[selector].SetActive(true);
			systems[selector].GetComponent<CelestialSystem>().OnWarpIn();

		}
		else
		if(tim > 0 && !isInWarp)
		{
			systems[selector].GetComponent<CelestialSystem>().OnWarpOut();
			warpSystem.SetActive(true);
			isInWarp = true;
			systems[selector].SetActive(false);
			warpSystem.GetComponent<CelestialSystem>().OnWarpIn();
		}
	}
}
