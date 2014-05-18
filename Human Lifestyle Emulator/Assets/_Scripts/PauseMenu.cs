using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast (ray, out hit, 200f)) 
			{	
				//Debug.DrawRay(ray.origin, ray.direction, Color.red, 40f);
				if(hit.collider != null)
				{
					if(hit.collider.gameObject.name == "MenuContinue")
					{
						GameObject.FindObjectOfType<GameManager>().UnPause();
					}
					else if(hit.collider.gameObject.name == "MenuReload")
					{
						GameObject.FindObjectOfType<GameManager>().UnPause();
						
						Application.LoadLevel(Application.loadedLevel);
					}	
					else if(hit.collider.gameObject.name == "MenuQuitToMenu")
					{
						//print ("recieving");
						Application.LoadLevel("MainMenu");
					}
				}
			}
		}
	}
}
