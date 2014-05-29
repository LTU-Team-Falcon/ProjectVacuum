using UnityEngine;
using System.Collections;

public class TextApear : MonoBehaviour {


	private BoxCollider col;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player")
			renderer.enabled = true;
	}

	void OnTriggerExit (Collider col)
	{
		renderer.enabled = false;
	}
}
