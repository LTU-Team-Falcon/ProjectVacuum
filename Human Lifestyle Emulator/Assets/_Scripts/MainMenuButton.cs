using UnityEngine;
using System.Collections;

public class MainMenuButton : MonoBehaviour 
{
	public bool doesToggle = false;
	public Color original;
	public Color toggled = new Color(1,0.77f, 0.44f, 1);
	public Color onClick = Color.red;
	public Color onSelect = Color.white;

	bool isHovering = false;
	// Use this for initialization
	void Start () 
	{
		original = guiTexture.color;
	}
	
	void OnMouseEnter() 
	{
		guiTexture.color = onSelect;
		isHovering = true;
	}

	void Update()
	{
		if(isHovering)
		{
			if(Input.GetMouseButtonDown(0))
			{
				guiTexture.color = onClick;
			} else
			if(Input.GetMouseButtonUp(0))
			{
				if(doesToggle)
				{
					Toggle();
					guiTexture.color = Color.Lerp(original, onClick, 0.5f);

				}
				else
				{		
					guiTexture.color = original;

				}
			}
		}
	}

	void Toggle()
	{
		Color temp = original;
		original = toggled;
		toggled = temp;
	}
	
	void OnMouseExit() 
	{
		guiTexture.color = original;
		isHovering = false;
	}

}
