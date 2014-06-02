using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VacuumShooter : MonoBehaviour 
{
	public List<string> StringsInBag = new List<string>();
	public List<GameObject> ObjectsInQueue = new List<GameObject>();
	public List<GameObject> CurrentArea = new List<GameObject>();
	

	public GameManager gameManager;

	public GameObject Pseudo;

	// Use this for initialization
	void Start () 
	{
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
			GameObject candidate = Resources.Load<GameObject>(StringsInBag[0]);
			if(candidate == null)
			{
				candidate = Resources.Load<GameObject>("Components/" + StringsInBag[0]);
			}

			GameObject toShoot = Instantiate(candidate) as GameObject;

			toShoot.transform.position = transform.position +	transform.forward*3;

			StringsInBag.RemoveAt(0);

			toShoot.AddComponent<GetScored>().gamemanager = gameManager;
			GetSucked sucker = toShoot.GetComponent<GetSucked>();
			sucker.DroppedFromIntake();
			Destroy(sucker);



			float Distance = Vector3.Distance(toShoot.transform.position, candidate.transform.position);
			int Distance2 = System.Convert.ToInt32(Distance);  
			GameObject.FindObjectOfType<Score>().playerScore += Distance2;

			//GameObject.FindObjectOfType<Score>().playerScore += 500;
	
			GameObject.FindObjectOfType<Score>().playerScore += 500;
			toShoot.rigidbody.AddExplosionForce(600 + toShoot.rigidbody.mass*500, transform.position, 10f);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
