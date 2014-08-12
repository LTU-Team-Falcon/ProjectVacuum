using UnityEngine;
using System.Collections;

public class CamNumCntr : MonoBehaviour {

	XinputHandler control;


	// Use this for initialization
	void Start () 
	{

		control = transform.parent.parent.gameObject.GetComponent<XinputHandler>();


		int PlayerNumber = control.indexNum + 1;//PlayerPrefs.GetInt("PlayerNum");
		int numberOfPlayers = control.gameManager.getNumberOfPlayers();
		//Camera thisCam = gameObject.GetComponent<Camera>();
		doIgnorance();
		//print("NumPlay = " + numberOfPlayers);
		//print("Play NUm = " + PlayerNumber);
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
				camera.rect = new Rect(0,.5f, 1, .5f);
			}
			else if(PlayerNumber == 2)
			{
				camera.rect = new Rect(0, 0, 1, .5f);
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
		else if(numberOfPlayers == 4)
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

	void doIgnorance()
	{
		//transform.parent.parent.FindChild("Player").gameObject.layer = 11 + control.indexNum;

		//Transform trans = transform.parent.parent.FindChild("Player");

		//camera.layerCullDistances[11+control.indexNum] = 0.1f;
		//camera.cullingMask = ~(1 <<  11+control.indexNum);
		//addChildrenToLayer(trans, 11+control.indexNum);
	}

	void addChildrenToLayer(Transform parTrans, int parLayer)
	{
		foreach(Transform child in parTrans)
		{
			child.gameObject.layer = parLayer;

			if(child.childCount >0)
			{
				addChildrenToLayer(child, parLayer);
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
