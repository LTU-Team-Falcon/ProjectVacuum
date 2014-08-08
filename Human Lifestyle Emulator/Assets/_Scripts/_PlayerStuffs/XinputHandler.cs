using UnityEngine;
using XInputDotNetPure; // Required in C#

public class XinputHandler : MonoBehaviour
{
	public bool isDebugging = false;

	bool usesKeyboard = false;

	XinputController cont;
	XinputKeyboard keyb;
	
	public int indexNum;
	
	// Use this for initialization
	void Awake()
	{
		cont = gameObject.GetComponent<XinputController>();
		
		keyb = gameObject.GetComponent<XinputKeyboard>();
		if(cont == null && keyb == null)
		{
			cont = gameObject.AddComponent<XinputController>();
		}
		else if(cont != null && cont.enabled == false)
		{
			cont.enabled = true;
		}

		if (cont != null)
		{
			cont.indexNum = this.indexNum;
			cont.isDebugging = this.isDebugging;
		}
	}

	void Start()
	{
/*		if(cont != null)
		{
	//		print("ContCalled");
			cont.CallUpdate();
		}
	//print("Awkaek");
		if(keyb == null && cont != null && cont.enabled && !cont.playerIndexSet)
		{
			if(GameObject.FindObjectOfType<XinputKeyboard>() == null)
			{
	//			print("Keyb established");
				keyb = gameObject.AddComponent<XinputKeyboard>();
				usesKeyboard = true;
				cont.enabled = false;
			}
		}*/
	}
	
	// Update is called once per frame
	void Update()
	{

	}



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
