using UnityEngine;
using System.Collections;

public class HoldRotationDirection : MonoBehaviour 
{
	Vector3 origRotation;
	// Use this for initialization
	void Start () 
	{
		origRotation = gameObject.transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.eulerAngles = origRotation;
	}
}
