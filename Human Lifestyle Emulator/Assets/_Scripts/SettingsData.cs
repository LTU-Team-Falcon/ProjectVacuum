using UnityEngine;
using System.Collections;

public class SettingsData : MonoBehaviour 
{
	public static bool _HasKeyboard = false;
	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(this);
		DontDestroyOnLoad(this.gameObject);
	}

	public void setHasKeyboard(bool par1bool)
	{
		_HasKeyboard = par1bool;
	}

	public bool getHasKeyboard()
	{
		return _HasKeyboard;
	}
}
