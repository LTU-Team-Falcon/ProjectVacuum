using UnityEngine;
using System.Collections;

public class CustomInputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string[] sticks = Input.GetJoystickNames();
		foreach(string str in sticks)
		{
			print(str);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
