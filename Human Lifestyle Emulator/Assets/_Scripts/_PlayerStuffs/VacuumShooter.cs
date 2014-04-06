using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VacuumShooter : MonoBehaviour 
{
	public List<string> StringsInBag = new List<string>();
	public List<GameObject> ObjectsInQueue = new List<GameObject>();

	public GameObject Pseudo;

	// Use this for initialization
	void Start () 
	{
		Debug.Log("loaded");
		bool isDone = false;
		for(int i = 0; isDone == false; i++)
		{
			if(PlayerPrefs.GetString("sucked" + i.ToString()) != "")
			{
				StringsInBag.Add(PlayerPrefs.GetString("sucked" + i.ToString()));
			}
			else
			{
				isDone = true;
			}
		}

	}

	public void ShootObject()
	{
		if(StringsInBag.Count != 0)
		{
			GameObject scorepart = Instantiate(Pseudo) as GameObject;
			scorepart.transform.position = transform.position +	transform.forward*2;

			scorepart.GetComponent<TextMesh>().text = StringsInBag[0];
			StringsInBag.RemoveAt(0);
	
			GameObject.FindObjectOfType<Score>().playerScore += 500;
			scorepart.rigidbody.AddExplosionForce(2500, transform.position, 10f);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
