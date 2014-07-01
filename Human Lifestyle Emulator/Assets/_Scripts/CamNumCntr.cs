using UnityEngine;
using System.Collections;

public class CamNumCntr : MonoBehaviour {



	// Use this for initialization
	void Start () {

		int PlayerNumber = PlayerPrefs.GetInt("PlayerNum");


		if (PlayerNumber == 1)
		{
			GameObject.Find("Main Camera").camera.rect = new Rect(0,0,1,1);
			GameObject.Find("Player2").SetActive(false);
			GameObject.Find("Player3").SetActive(false);
			GameObject.Find("Player4").SetActive(false);
		}
		else if(PlayerNumber == 2)
		{
			if (transform.parent.tag == "Player")
			{
				camera.rect = new Rect(0,.5f,1,1);
				Debug.Log("doing Camera for" + transform.parent.tag);
			}
			else if (transform.parent.tag == "Player2")
			{
				camera.rect = new Rect(0,-.5f,1,1);
				Debug.Log("doing Camera for" + transform.parent.tag);
			}

			GameObject.Find("Player3").SetActive(false);
			GameObject.Find("Player4").SetActive(false);

		}
		else if(PlayerNumber == 3)
		{
			if (transform.parent.tag == "Player")
			{
				camera.rect = new Rect(0,.5f,1,1);
			}
			else if (transform.parent.tag == "Player2")
			{
				camera.rect = new Rect(0,-.5f,.5f,1);
			}
			else if (transform.parent.tag == "Player3")
			{
				camera.rect = new Rect(.5f,-.5f,1,1);
			}
			GameObject.Find("Player4").SetActive(false);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
