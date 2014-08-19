using UnityEngine;
using System.Collections;

public class CelestialSystem : MonoBehaviour 
{
	public Color AmbientLight;

	// Use this for initialization
	void Start () 
	{
		RenderSettings.ambientLight = AmbientLight;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void OnWarpOut()
	{
		if(gameObject.name == "SunSystem")
		{
			gameObject.GetComponent<SunSystem>().OnWarpOut();
		}
		else
		if(gameObject.name == "Mars&AsteroidBeltSystem")
		{
			
		}
	}

	public void OnWarpIn()
	{
		if(gameObject.name == "SunSystem")
		{
			gameObject.GetComponent<SunSystem>().OnWarpIn();
		}
		else
		if(gameObject.name == "Mars&AsteroidBeltSystem")
		{
			
		}

	}


}
