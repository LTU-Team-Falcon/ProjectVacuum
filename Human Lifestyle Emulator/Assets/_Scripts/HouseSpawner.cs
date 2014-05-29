using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HouseSpawner : MonoBehaviour {

	public List<GameObject> Houses = new List<GameObject>();

	private int HouseNum;
	// Use this for initialization
	void Start () {

		HouseNum = Houses.Count;

		int j = 0;

		Debug.Log("Starting");
		for (int i = 0; i<HouseNum;i++)
		{
			int RandomHouse = Random.Range(0,HouseNum);

			//positve side
			if (i < HouseNum/2)
			{
				Debug.Log("< half");
				Instantiate(Houses[RandomHouse],new Vector3(14.5f+(i*40f),0f,34f),Quaternion.identity);
			}
			else
			{
				Debug.Log("> half");
				Instantiate(Houses[RandomHouse],new Vector3(14.5f+(j*40f),0f,-34f),Quaternion.Euler(0,180,0));
				j++;
			}


		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
