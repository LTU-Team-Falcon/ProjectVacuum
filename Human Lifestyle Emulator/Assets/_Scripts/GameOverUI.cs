using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour {

	public GUIText PlayAgain;
	public GUIText Quit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (0)) 
		{
			if (PlayAgain.HitTest (Input.mousePosition)) 
			{
				Application.LoadLevel (Application.loadedLevel);
			}
			if (Quit.HitTest (Input.mousePosition)) 
			{
			Application.Quit ();
			}
		}
	}
}
