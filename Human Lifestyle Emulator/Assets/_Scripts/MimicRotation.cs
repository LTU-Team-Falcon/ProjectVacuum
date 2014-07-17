using UnityEngine;
using System.Collections;

public class MimicRotation : MonoBehaviour {
	public Transform turgetTransform;

	public Vector3 mimicFactor = new Vector3(0,.75f,0);



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 calc = turgetTransform.localEulerAngles;

		if(calc.x > 180f)
		{
			calc.x -= 360f;
		}

		this.transform.localEulerAngles = Vector3.Scale(calc, mimicFactor);
	}
}
