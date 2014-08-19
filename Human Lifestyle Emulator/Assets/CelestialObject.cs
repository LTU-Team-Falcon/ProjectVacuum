using UnityEngine;
using System.Collections;

public class CelestialObject : MonoBehaviour 
{

	public Vector3 revolutions;
	public float revSpeed = 1;
	

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.eulerAngles += revolutions * revSpeed;
	}

	void OnTriggerExit(Collider col)
	{

	}
}
