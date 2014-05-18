using UnityEngine;
using System.Collections;

public class Transparency : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Color color = renderer.material.color;
		color.a -= 0.5f;
		renderer.material.color = color;


		//gameObject.renderer.material.color.a =.5f;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
