using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnablePowerUps : MonoBehaviour {

	public List<GameObject> Children;

	public int count = 0;
	public bool PickedUp;

	// Use this for initialization

	void Start () {

		foreach (Transform child in transform) {
			Children.Add(child.gameObject);
		}
		count = 100;
	}
	
	// Update is called once per frame
	void Update () {

		//if (PickedUp == true) 
		//{
			//count++;
		//}

		if (count == 1250)
		{
			foreach (GameObject child in Children)
			{
				child.gameObject.SetActive(true);
			}
			renderer.enabled = true;
			collider.enabled = true;
			PickedUp = false;
			count = 0;
		}
		else if (PickedUp == true)
		{
			count++;
		}
	}


	void OnTriggerEnter(Collider col) {

		if (col.gameObject.tag == "Player")
		{
			foreach (GameObject child in Children)
			{
				child.gameObject.SetActive(false);
			}
			renderer.enabled = false;
			collider.enabled = false;
			PickedUp = true;
		}
	}

}
