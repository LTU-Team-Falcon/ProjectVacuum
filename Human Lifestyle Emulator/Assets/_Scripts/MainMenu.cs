using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public GUITexture Main_Play_Off;
	public GUITexture Main_Quit_Off;
	public GUITexture Main_Settings_Off;

	public GameObject subMenuSettings;
	public GameObject subMenuMain;

	public GUITexture Sett_Back_Off;
	public GUITexture Sett_Keyboard_Off;

	SettingsData settingsData;

	void Start () 
	{
		settingsData = GameObject.FindObjectOfType<SettingsData>();
	}

	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonUp (0)) 
		{
			if(subMenuMain.activeInHierarchy)
			{
				if (Main_Play_Off.HitTest (Input.mousePosition)) 
				{
					Application.LoadLevel (1);
				} else
					if (Main_Settings_Off.HitTest (Input.mousePosition))
				{
					
					subMenuMain.SetActive(false);
					subMenuSettings.SetActive(true);
					return;
					// hid main menu and unhid settings menu
				} else
				if (Main_Quit_Off.HitTest (Input.mousePosition)) 
				{
					Application.Quit ();
				}

			}



			if(subMenuSettings.activeInHierarchy)
			{
				if (Sett_Keyboard_Off.HitTest (Input.mousePosition))
				{
					settingsData._HasKeyboard = !settingsData._HasKeyboard;
					//this whole set up is super temporary, unless you like it, Spangler.
				} else 
					if(Sett_Back_Off.HitTest(Input.mousePosition))
				{
					subMenuSettings.SetActive(false);
					subMenuMain.SetActive(true);
					return;
				}
			}

		}
	}

	void fullyToggleActivation(GameObject parGameObj, bool parbool)
	{

	}

}
