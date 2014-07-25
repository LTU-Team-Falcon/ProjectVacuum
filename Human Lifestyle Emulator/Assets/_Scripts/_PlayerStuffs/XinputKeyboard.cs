using UnityEngine;
using System.Collections;

public class XinputKeyboard : MonoBehaviour 
{
	public bool isDebugging = false;
	public int indexNum = 1;

	// Use this for initialization
	void Start () 
	{
	
	}


	public Vector2 GetLeftStick()
	{
		return new Vector2(Input.GetAxis("Horizontal") , Input.GetAxis("Vertical"));
	}
	public Vector2 GetLastLeftStick()
	{
		return new Vector2(Input.GetAxis("Horizontal") , Input.GetAxis("Vertical"));
	}



	public Vector2 GetRightStick()
	{
		return new Vector2(Input.GetAxis("Mouse X") , Input.GetAxis("Mouse Y"));
	}
	public Vector2 GetLastRightStick()
	{
		return new Vector2(Input.GetAxis("Mouse X") , Input.GetAxis("Mouse Y"));
	}
	


	public float GetLeftTrigger()
	{
		if (Input.GetButton("Fire2"))
			return 1f;

		return 0f;
	}
	public float GetLastLeftTrigger()
	{
		return 0f;
	}
	
	
	
	public float GetRightTrigger()
	{
		if (Input.GetButton("Fire1"))
			return 1f;
		
		return 0f;
	}
	public float GetLastRightTrigger()
	{
		return 0f;
	}
	
	
	
	public void SetVibration(Vector2 par1Vec)
	{
		
	}

















	public bool GetButtonDown(string parButName)
	{
		if(parButName == "A")
		{
			return Input.GetButtonDown("Jump");
		}
		else if(parButName == "B")
		{
			return Input.GetButtonDown("Fire3");
		}
		else if(parButName == "X")
		{
		}
		else if(parButName == "Y")
		{
			return Input.GetButtonDown("SwitchWeapons");
		}
		else if(parButName == "LB")
		{
			return Input.GetButtonDown("Dash");
		}
		else if(parButName == "RB")
		{
			return Input.GetButtonDown("Brick");
		}
		else if(parButName == "RightStick")
		{
		}
		else if(parButName == "LeftStick")
		{
		}
		else if(parButName == "Start")
		{
			return Input.GetButtonDown("Pause");
		}

		return GetButtonDown(parButName);
	}

	public bool GetButton(string parButName)
	{
		if(parButName == "A")
		{
			return Input.GetButton("Jump");
		}
		else if(parButName == "B")
		{
			return Input.GetButton("Fire3");
		}
		else if(parButName == "X")
		{
		}
		else if(parButName == "Y")
		{
			return Input.GetButton("SwitchWeapons");
		}
		else if(parButName == "LB")
		{
			return Input.GetButton("Dash");
		}
		else if(parButName == "RB")
		{
			return Input.GetButton("Brick");
		}
		else if(parButName == "RightStick")
		{
		}
		else if(parButName == "LeftStick")
		{
		}
		else if(parButName == "Start")
		{
			return Input.GetButton("Pause");
		}
		
		return GetButton(parButName);
	}


	public bool GetButtonUp(string parButName)
	{
		if(parButName == "A")
		{
			return Input.GetButtonUp("Jump");
		}
		else if(parButName == "B")
		{
			return Input.GetButtonUp("Fire3");
		}
		else if(parButName == "X")
		{
		}
		else if(parButName == "Y")
		{
			return Input.GetButtonUp("SwitchWeapons");
		}
		else if(parButName == "LB")
		{
			return Input.GetButtonUp("Dash");
		}
		else if(parButName == "RB")
		{
			return Input.GetButtonUp("Brick");
		}
		else if(parButName == "RightStick")
		{
		}
		else if(parButName == "LeftStick")
		{
		}
		else if(parButName == "Start")
		{
			return Input.GetButtonUp("Pause");
		}
		
		return GetButtonUp(parButName);
	}

	public bool GetLastButton( string parButtName)
	{
		return false;
	}





	// Update is called once per frame
	void Update () {
	
	}
}
