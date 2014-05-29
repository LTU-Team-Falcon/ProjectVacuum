using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListingObjects : MonoBehaviour {

	public List<Transform> ObjectsInRoom = new List<Transform>();


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Environment")
		{
			IsSubPiece Script = other.GetComponent<IsSubPiece>();
			HasSubPiece Script2= other.GetComponent<HasSubPiece>();
			if (Script == null || Script2 != null)
			{
				ObjectsInRoom.Add(other.transform);
			}
		}

	}
}
