using UnityEngine;
using System.Collections;

public class DeathCamCntr : MonoBehaviour {

	public int numberOfPlayers;
	public int PlayerNumber;

	// Use this for initialization
	void Start () {

		numberOfPlayers = GameObject.FindGameObjectsWithTag ("Player").Length;

		if (gameObject.name == "P1DeathCam")
		{
			PlayerNumber = 1;
		}
		else if (gameObject.name == "P2DeathCam")
		{
			PlayerNumber = 2;
		}
		else if (gameObject.name == "P3DeathCam")
		{
			PlayerNumber = 3;
		}
		else if (gameObject.name == "P4DeathCam")
		{
			PlayerNumber = 4;
		}


		if(numberOfPlayers == 1)
		{
			if(PlayerNumber == 1)
			{
				camera.rect = new Rect(0,0,1,1);
			}
			else
			{
			//	transform.parent.parent.gameObject.SetActive(false);
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
			//	transform.parent.parent.gameObject.SetActive(false);
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
//				transform.parent.parent.gameObject.SetActive(false);
			}
		}
		else if(numberOfPlayers == 4)
		{
			if(PlayerNumber == 1)
			{
				this.gameObject.camera.rect = new Rect(0, 0.5f, 0.5f,1);
			}
			else if(PlayerNumber == 2)
			{
				this.gameObject.camera.rect = new Rect(0.5f,0.5f,0.5f,1);
			}
			else if(PlayerNumber == 3)
			{
				this.gameObject.camera.rect = new Rect(0,-0.5f,0.5f,1);
			}
			else if(PlayerNumber == 4)
			{
				this.gameObject.camera.rect = new Rect(0.5f,-0.5f,0.5f,1);
			}
			else
			{
			//	transform.parent.parent.gameObject.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		numberOfPlayers = GameObject.FindGameObjectsWithTag ("Player").Length;

	}
}
