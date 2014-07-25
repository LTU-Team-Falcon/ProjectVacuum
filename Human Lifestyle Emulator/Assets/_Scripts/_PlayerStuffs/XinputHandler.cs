using UnityEngine;
using XInputDotNetPure; // Required in C#

public class XinputHandler : MonoBehaviour
{
	public bool isDebugging = false;

	bool usesKeyboard = false;

	XinputController cont;
	XinputKeyboard keyb;
	
	public int indexNum = 1;
	
	// Use this for initialization
	void Start()
	{
		cont = gameObject.GetComponent<XinputController>();

		keyb = gameObject.GetComponent<XinputKeyboard>();

		if(cont != null)
		{
			this.indexNum = cont.indexNum;
			this.isDebugging = cont.isDebugging;
		}
		else
		{
			this.indexNum = keyb.indexNum;
			this.isDebugging = keyb.isDebugging;
			usesKeyboard = true;
		}
		// No need to initialize anything for the plugin
	}
	
	// Update is called once per frame
	void Update()
	{
		// Find a PlayerIndex, for a single player game
		// Will find the controller in slot "indexNum" and use it

/*		
		// Detect if a button was pressed this frame
		if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
		{
			renderer.material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
		}
		// Detect if a button was released this frame
		if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
		{
			renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
		
		// Set vibration according to triggers
		GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);
		
		// Make the current object turn
		transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);
*/	}



	public Vector2 GetLeftStick()
	{
		if(usesKeyboard)
			return keyb.GetLeftStick();
		else
			return cont.GetLeftStick();
	}
	public Vector2 GetLastLeftStick()
	{
		if(usesKeyboard)
			return keyb.GetLastLeftStick();
		else
			return cont.GetLastLeftStick();
	}



	public Vector2 GetRightStick()
	{
		if(usesKeyboard)
			return keyb.GetRightStick();
		else
			return cont.GetRightStick();
	}
	public Vector2 GetLastRightStick()
	{
		if(usesKeyboard)
			return keyb.GetLastRightStick();
		else
			return cont.GetLastRightStick();
	}



	public float GetLeftTrigger()
	{
		if(usesKeyboard)
			return keyb.GetLeftTrigger();
		else
			return cont.GetLeftTrigger();
	}
	public float GetLastLeftTrigger()
	{
		if(usesKeyboard)
			return keyb.GetLastLeftTrigger();
		else
			return cont.GetLastLeftTrigger();
	}



	public float GetRightTrigger()
	{
		if(usesKeyboard)
			return keyb.GetRightTrigger();
		else
			return cont.GetRightTrigger();
	}
	public float GetLastRightTrigger()
	{
		if(usesKeyboard)
			return keyb.GetLastRightTrigger();
		else
			return cont.GetLastRightTrigger();
	}



	public void SetVibration(Vector2 par1Vec)
	{
		if(usesKeyboard)
			 keyb.SetVibration( par1Vec);
		else
			 cont.SetVibration( par1Vec);
	}



	public bool GetButtonDown(string parButtName)
	{
		if(usesKeyboard)
			return keyb.GetButtonUp( parButtName);
		else
			return cont.GetButtonUp( parButtName);
	}

	public bool GetButtonUp(string parButtName)
	{
		if(usesKeyboard)
			return keyb.GetButtonUp( parButtName);
		else
			return cont.GetButtonUp( parButtName);
	}


	public bool GetButton(string parButName)
	{
		if(usesKeyboard)
			return keyb.GetButton( parButName);
		else
			return cont.GetButton( parButName);


	}


	public bool GetLastButton(string parButName)
	{		
		if(usesKeyboard)
			return keyb.GetLastButton( parButName);
		else
			return cont.GetLastButton( parButName);
	}


	void OnGUI()
	{

	}
}
