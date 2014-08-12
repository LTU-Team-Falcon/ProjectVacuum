using UnityEngine;
using System.Collections;

public class SettingsData : MonoBehaviour 
{
	public bool _HasKeyboard = false;
	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(this);
		DontDestroyOnLoad(this.gameObject);
	}

}
