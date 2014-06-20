using UnityEngine;
using System.Collections;

public class NPC_Sucking : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AddToIntake(GameObject parGameObj)
	{	//adds specified object to the stuck at intake list
		parGameObj.transform.parent = transform;
		parGameObj.GetComponent<GetSucked>().AddedToIntake();
		parGameObj.collider.enabled = false;

		
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Suckable")
		{
			AddToIntake(col.gameObject);
			col.gameObject.SetActive(false);
		}
		
		
	}
}
