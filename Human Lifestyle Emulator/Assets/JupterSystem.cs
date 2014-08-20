using UnityEngine;
using System.Collections;

public class JupterSystem : MonoBehaviour 
{
	public Vector2 minMax = new Vector2(1111,2632);

	GameObject jupiterPrime;
	// Use this for initialization
	void Start () 
	{
		jupiterPrime = transform.FindChild("JupiterRotation").FindChild("Jupiter").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(jupiterPrime.transform.position.magnitude > minMax.y)
		{
			minMax = new Vector2(minMax.x, jupiterPrime.transform.position.magnitude);
		}

		if(jupiterPrime.transform.position.magnitude < minMax.x)
		{
			minMax = new Vector2(jupiterPrime.transform.position.magnitude, minMax.y);

		}
	}
}
