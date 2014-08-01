using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour 
{
	[HideInInspector]
	public pauseUnpause pauseHandler;
	XinputHandler inputHandler;
	public Transform selector;
	List<Transform> MenuOptions = new List<Transform>();
	int currentSelection = 0;

	// Use this for initialization
	void Start () 
	{
		MenuOptions.Add(transform.FindChild("MenuContinue"));
		MenuOptions.Add(transform.FindChild("MenuReload"));
		MenuOptions.Add(transform.FindChild("MenuQuitToMenu"));

		inputHandler = transform.parent.parent.gameObject.GetComponent<XinputHandler>();

		selector = transform.FindChild("select");
	}

	// Update is called once per frame
	bool canChange = true;
	void Update()
	{
		if(canChange)
		{
			if(inputHandler.GetLeftStick().y > 0 )
			{
				currentSelection --;
				canChange = false;
			}
			else if(inputHandler.GetLeftStick().y < 0)
			{
				currentSelection ++;
				canChange = false;
			}

			if(currentSelection == -1)
			{
				currentSelection = MenuOptions.Count - 1;
			}
			else if(currentSelection == MenuOptions.Count)
			{
				currentSelection = 0;
			}
		}
		else
		{

			selector.localPosition = Vector3.Lerp(selector.localPosition, new Vector3(selector.localPosition.x, MenuOptions[currentSelection].localPosition.y, selector.localPosition.z) , 0.5f);
			if(			Mathf.Approximately(MenuOptions[currentSelection].localPosition.y , selector.localPosition.y) )
			{
				canChange = true;
			}
		}
	}

	void LateUpdate () 
	{

		if(inputHandler.GetButtonUp("A"))
		{
			if(currentSelection == 2)
			{
				Application.Quit();
			}
			else if(currentSelection == 1)
			{
				Application.LoadLevel(Application.loadedLevel);
			}
			else
			{
				pauseHandler.Unpause();
			}
		}
		/*if(Input.GetMouseButton(0))
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
		}*/

	}
}
