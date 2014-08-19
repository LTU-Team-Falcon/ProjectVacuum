using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SunHeat : MonoBehaviour 
{
	int iNum = 20;
	bool isFirstTick = true;
	// Use this for initialization
	void OnEnable () 
	{
/*		transform.localScale *= 1000f;
*/	}

	void OnDisable()
	{
/*		transform.localScale *= 1000f;
		transform.localScale /= 1000f;
		transform.localScale /= 1000f;
*/

	}
	
	// Update is called once per frame
	void Update () 
	{
/*		if(isFirstTick)
		{
			transform.localScale /= 1000f;
			isFirstTick = false;
		}*/
	}

	void OnTriggerEnter(Collider col)
	{
			if(col.gameObject.name == "Player")
			{
				OutSun(col.gameObject);
			}
	//	}
	}

	
	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.name == "Player")
		{
			InSun(col.gameObject);	
		}
	}

	public void InSun(GameObject gamObj)
	{
		gamObj.AddComponent<DamageOverTime>();
		PixelizeCamera pixy = gamObj.transform.parent.FindChild("HandCube").GetComponentInChildren<PixelizeCamera>();
		pixy.farRad = 13f;
		pixy.maxResPcent = 0.1f;
		
		pixy.shouldUpdate = true;
	}

	public void OutSun(GameObject gamObj)
	{
		DamageOverTime dmgOverTime = gamObj.GetComponent<DamageOverTime>();
		if(dmgOverTime != null)
		{
			Destroy(dmgOverTime);
			
		}
		PixelizeCamera pixy = gamObj.transform.parent.FindChild("HandCube").GetComponentInChildren<PixelizeCamera>();
		pixy.farRad = 30f;
		pixy.maxResPcent = 0.17f;
		pixy.shouldUpdate = true;

	}




}
