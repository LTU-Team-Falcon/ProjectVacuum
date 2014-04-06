using UnityEngine;
using System.Collections;

public class WorkToDoScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		string t = "THINGS TO DO:";
		t += "\n 1) " + "Implement new Assets";
		t += "\n 2) " + "Implement awkward controls for vacuum (using torque and force to move it)";
		t += "\n 3) " + "Implement system that organizes/reduces the list of objects that have been sucked up;";
		t += "\n " + "like so that you shoot whole objects and not just broken pieces";
		t += "\n 4) " + "Implement system for holographic objects and scoring";
		t += "\n 5) " + "Implement objects break due to force rather than ''damage'' ";
		t += "\n 6) " + "Implement System for spawning objects from their names";
		gameObject.GetComponent<TextMesh>().text = t;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
