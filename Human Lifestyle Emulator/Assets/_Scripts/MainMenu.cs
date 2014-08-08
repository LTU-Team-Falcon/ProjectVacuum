using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public GUITexture Play_Off;
	public GUITexture Quit_Off;
	public GUITexture Settings_Off;

	Color Orginal;

	// Use this for initialization
	void Start () {

		Orginal = guiTexture.color;

	}

	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) 
		{
			if (Play_Off.HitTest (Input.mousePosition)) 
			{
				Application.LoadLevel (1);
			}
			if (Settings_Off.HitTest (Input.mousePosition))
			{
				// hid main menu and unhid settings menu
			}

			if (Quit_Off.HitTest (Input.mousePosition)) 
			{
				Application.Quit ();
			}
		}

			
	}

	void OnMouseEnter() {
		guiTexture.color = Color.white;

	}

	void OnMouseExit() {
		guiTexture.color = Orginal;
	}

}
