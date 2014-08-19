using UnityEngine;
using System.Collections;

public class SunSystem : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnWarpIn()
	{
		PhysicsFPSWalker[] phys = GameObject.FindObjectsOfType<PhysicsFPSWalker>();
		
		foreach(PhysicsFPSWalker physWalk in phys)
		{
			gameObject.GetComponentInChildren<SunHeat>().InSun(physWalk.gameObject);
		}
	}

	public void OnWarpOut()
	{
		PixelizeCamera pixy = this.gameObject.AddComponent<PixelizeCamera>();
		pixy.maxResPcent = 1;

		PhysicsFPSWalker[] phys = GameObject.FindObjectsOfType<PhysicsFPSWalker>();

		foreach(PhysicsFPSWalker physWalk in phys)
		{
			GoneSun(physWalk.gameObject);
		}
	}


	public void GoneSun(GameObject gamObj)
	{
		DamageOverTime dmgOverTime = gamObj.GetComponent<DamageOverTime>();
		if(dmgOverTime != null)
		{
			Destroy(dmgOverTime);
		}
		PixelizeCamera pixy = gamObj.transform.parent.FindChild("HandCube").GetComponentInChildren<PixelizeCamera>();
		pixy.farRad = 30f;
		pixy.maxResPcent =	1f;
		pixy.antiAlias = 1;
		pixy.shouldUpdate = true;
	}
}
