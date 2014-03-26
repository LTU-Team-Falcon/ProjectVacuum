using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
 
 	void ClickedOnSomethin()
 	{
 		
 	}
	
	// Update is called once per frame
	void Update () 
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
						Application.LoadLevel("TestScene");
					}
					else if(hit.collider.gameObject.name == "MenuQuitGame")
					{
						Application.Quit();
					}
				}
			}
		}
	}
}
