using UnityEngine;
using System.Collections;

public class CamNumCntr : MonoBehaviour {

	XinputHandler control;

	// Use this for initialization
	void Start () 
	{
		//if(transform.parent.gameObject.GetComponent<XinputHandler>() != null)
		//{
			control = transform.parent.parent.gameObject.GetComponent<XinputHandler>();
		//}


		int PlayerNumber = control.indexNum + 1;//PlayerPrefs.GetInt("PlayerNum");
		int numberOfPlayers = Input.GetJoystickNames().Length;
		//Camera thisCam = gameObject.GetComponent<Camera>();

		if(numberOfPlayers == 1)
		{
			if(PlayerNumber == 1)
			{
				camera.rect = new Rect(0,0,1,1);
			}
			else
			{
				transform.parent.parent.gameObject.SetActive(false);
			}
		}
		else if(numberOfPlayers == 2)
		{
			if(PlayerNumber == 1)
			{
				camera.rect = new Rect(0,.5f,1,1);
			}
			else if(PlayerNumber == 2)
			{
				camera.rect = new Rect(0,-.5f,1,1);
			}
			else
			{
				transform.parent.parent.gameObject.SetActive(false);
			}
		}
		else if(numberOfPlayers == 3)
		{
			if(PlayerNumber == 1)
			{
				camera.rect = new Rect(0,0.5f,1,1);
			}
			else if(PlayerNumber == 2)
			{
				camera.rect = new Rect(0,-0.5f,0.5f,1);
			}
			else if(PlayerNumber == 3)
			{
				camera.rect = new Rect(0.5f,-0.5f,0.5f,1);
			}
			else
			{
				transform.parent.parent.gameObject.SetActive(false);
			}
		}
		else if(numberOfPlayers ==4)
		{
			if(PlayerNumber == 1)
			{
				camera.rect = new Rect(0, 0.5f, 0.5f,1);
			}
			else if(PlayerNumber == 2)
			{
				camera.rect = new Rect(0.5f,0.5f,0.5f,1);
			}
			else if(PlayerNumber == 3)
			{
				camera.rect = new Rect(0,-0.5f,0.5f,1);
			}
			else if(PlayerNumber == 4)
			{
				camera.rect = new Rect(0.5f,-0.5f,0.5f,1);
			}
			else
			{
				transform.parent.parent.gameObject.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
