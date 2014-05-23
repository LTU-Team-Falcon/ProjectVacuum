using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public bool atMenu = true;
	public bool logoDone = false;
	Light spotty;
	float idealIntensity;
	
	// Use this for initialization
	void Start () 
	{
		spotty = GameObject.FindGameObjectWithTag("Light").light;
		idealIntensity = spotty.intensity;
		//spotty.intensity = 0;
	}
 
	void InitMenu()
	{
		atMenu = true;
	}
	
	void StartMovements()
	{
		logoDone = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(atMenu)
		{
			if(Input.GetMouseButtonUp(0))
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				
				if (Physics.Raycast (ray, out hit, 100f)) 
				{	
					if(hit.collider != null)
					{
						if(hit.collider.gameObject.name == "MenuStartLevel")
						{
							Application.LoadLevel("TestScene2");
						}
						else if(hit.collider.gameObject.name == "MenuQuitGame")
						{
							Application.Quit();
						}
					}
				}
			}
		}
		else if(logoDone)
		{
			//spotty.intensity += (idealIntensity - spotty.intensity)/idealIntensity;
		}
		else
		{
			
		}
	}
}
