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

/*		PixelizeCamera pixy = this.gameObject.GetComponent<PixelizeCamera>();
		if(pixy == null)
		{
			pixy = this.gameObject.AddComponent<PixelizeCamera>();
		}
*/
		foreach(PhysicsFPSWalker physWalk in phys)
		{
			gameObject.GetComponentInChildren<SunHeat>().InSun(physWalk.gameObject);
		}
	}

	public void OnWarpOut()
	{
/*		PixelizeCamera pixy = this.gameObject.GetComponent<PixelizeCamera>();
		pixy.farRad = 10000f;
		pixy.maxResPcent =	1f;
		pixy.antiAlias = 2;
		pixy.shouldUpdate = true;
*/		

		PhysicsFPSWalker[] phys = GameObject.FindObjectsOfType<PhysicsFPSWalker>();

		foreach(PhysicsFPSWalker physWalk in phys)
		{
			GoneSun(physWalk.gameObject);
		}
	}


	public void GoneSun(GameObject gamObj)
	{
		print("onwarpOut");
		PixelizeCamera pixy = gamObj.transform.parent.FindChild("HandCube").GetComponentInChildren<PixelizeCamera>();
		pixy.farRad = 10000f;
		pixy.numLOD = 1;
		pixy.maxResPcent =	1f;
		pixy.antiAlias = 4;
		pixy.shouldUpdate = true;

		DamageOverTime dmgOverTime = gamObj.GetComponent<DamageOverTime>();
		if(dmgOverTime != null)
		{
			Destroy(dmgOverTime);
		}
	}
}
