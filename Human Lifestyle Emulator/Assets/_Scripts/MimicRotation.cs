using UnityEngine;
using System.Collections;

public class MimicRotation : MonoBehaviour {
	public Transform turgetTransform;

	public Vector3 mimicFactor = new Vector3(0,1,0);

	public bool mimicTrueRotation = false;
	public bool changeTrueRotation = false;
	public bool calcOffset = false;

	 Vector3 origRotOffset = new Vector3(0,0,0);



	// Use this for initialization
	void Awake () {
	
		if(calcOffset)
		{
			Vector3 vec1 = transform.localEulerAngles;
			if(changeTrueRotation)
			{
				vec1 = transform.eulerAngles;
			}

			Vector3 vec2 = turgetTransform.localEulerAngles;
			if(mimicTrueRotation)
			{
				vec2 = turgetTransform.eulerAngles;
			}

			origRotOffset = vec1 - vec2;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 calc = turgetTransform.localEulerAngles;
		if(mimicTrueRotation)
		{
			calc = turgetTransform.eulerAngles;
		}

		calc += origRotOffset;

		if(calc.x > 180f)
		{
			calc.x -= 360f;
		}

		if(changeTrueRotation)
		{
			this.transform.eulerAngles = Vector3.Scale(calc, mimicFactor) + origRotOffset;
		}
		else
		{
			this.transform.localEulerAngles = Vector3.Scale(calc, mimicFactor) + origRotOffset;
		}
	}
}
