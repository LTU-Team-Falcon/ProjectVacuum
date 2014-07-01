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
		//spotty = GameObject.FindGameObjectWithTag("Light").light;
		//idealIntensity = spotty.intensity;
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
							GameObject.Find("MenuStartLevel").renderer.enabled = false;
							GameObject.Find("MenuQuitGame").renderer.enabled = false;
							GameObject.Find("MenuStartLevel").collider.enabled = false;
							GameObject.Find("MenuQuitGame").collider.enabled = false;

							GameObject.Find("MenuBack").renderer.enabled = true;
							GameObject.Find("MenuPlayGame").renderer.enabled = true;
							GameObject.Find("MenuBack").collider.enabled = true;
							GameObject.Find("MenuPlayGame").collider.enabled = true;

							GameObject.Find("MenuPlayerText").renderer.enabled = true;
							GameObject.Find("1PlayersText").renderer.enabled = true;
							GameObject.Find("1PlayersText").collider.enabled = true;
							GameObject.Find("2PlayersText").renderer.enabled = true;
							GameObject.Find("2PlayersText").collider.enabled = true;
							GameObject.Find("3PlayersText").renderer.enabled = true;
							GameObject.Find("3PlayersText").collider.enabled = true;
							GameObject.Find("4PlayersText").renderer.enabled = true;
							GameObject.Find("4PlayersText").collider.enabled = true;



						}
						else if(hit.collider.gameObject.name == "MenuQuitGame")
						{
							Application.Quit();
						}
						else if(hit.collider.gameObject.name == "MenuBack")
						{
							GameObject.Find("MenuBack").renderer.enabled = false;
							GameObject.Find("MenuPlayGame").renderer.enabled = false;
							GameObject.Find("MenuBack").collider.enabled = false;
							GameObject.Find("MenuPlayGame").collider.enabled = false;

							GameObject.Find("MenuStartLevel").renderer.enabled = true;
							GameObject.Find("MenuQuitGame").renderer.enabled = true;
							GameObject.Find("MenuStartLevel").collider.enabled = true;
							GameObject.Find("MenuQuitGame").collider.enabled = true;

							GameObject.Find("MenuPlayerText").renderer.enabled = false;
							GameObject.Find("1PlayersText").renderer.enabled = false;
							GameObject.Find("1PlayersText").collider.enabled = false;
							GameObject.Find("2PlayersText").renderer.enabled = false;
							GameObject.Find("2PlayersText").collider.enabled = false;
							GameObject.Find("3PlayersText").renderer.enabled = false;
							GameObject.Find("3PlayersText").collider.enabled = false;
							GameObject.Find("4PlayersText").renderer.enabled = false;
							GameObject.Find("4PlayersText").collider.enabled = false;
						}
						else if(hit.collider.gameObject.name == "MenuPlayGame")
						{
							Application.LoadLevel(1);
						}
						else if(hit.collider.gameObject.name == "1PlayersText")
						{
							PlayerPrefs.SetInt("PlayerNum", 1);
							GameObject.Find("1PlayersText").renderer.material.color = Color.white;
							GameObject.Find("2PlayersText").renderer.material.color = Color.gray;
							GameObject.Find("3PlayersText").renderer.material.color = Color.gray;
							GameObject.Find("4PlayersText").renderer.material.color = Color.gray;
							PlayerPrefs.Save();
						}
						else if(hit.collider.gameObject.name == "2PlayersText")
						{
							PlayerPrefs.SetInt("PlayerNum", 2);
							GameObject.Find("2PlayersText").renderer.material.color = Color.white;
							GameObject.Find("1PlayersText").renderer.material.color = Color.gray;
							GameObject.Find("3PlayersText").renderer.material.color = Color.gray;
							GameObject.Find("4PlayersText").renderer.material.color = Color.gray;
							PlayerPrefs.Save();
						}
						else if(hit.collider.gameObject.name == "3PlayersText")
						{
							PlayerPrefs.SetInt("PlayerNum", 3);
							GameObject.Find("3PlayersText").renderer.material.color = Color.white;
							GameObject.Find("1PlayersText").renderer.material.color = Color.gray;
							GameObject.Find("2PlayersText").renderer.material.color = Color.gray;
							GameObject.Find("4PlayersText").renderer.material.color = Color.gray;
							PlayerPrefs.Save();
						}
						else if(hit.collider.gameObject.name == "4PlayersText")
						{
							PlayerPrefs.SetInt("PlayerNum", 4);
							GameObject.Find("4PlayersText").renderer.material.color = Color.white;
							GameObject.Find("1PlayersText").renderer.material.color = Color.gray;
							GameObject.Find("2PlayersText").renderer.material.color = Color.gray;
							GameObject.Find("3PlayersText").renderer.material.color = Color.gray;
							PlayerPrefs.Save();
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
