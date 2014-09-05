using UnityEngine;
using System.Collections;

public class FaceVelocity : MonoBehaviour 
{
	public Rigidbody targetRigid;
	public bool usesInputAsCompass = true;
	 public XinputHandler control;

	public bool localEulers = true;



	public Vector3 MirrorAxis;
	public float lerpSpeed;
	public bool returnToDefault = false;
	public Vector3 defaultRot;

	public Vector3 target;


	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(usesInputAsCompass)
		{
			Vector2 sticks = control.GetLeftStick();
			target = Angle2Vector3s(Vector3.zero, new Vector3( sticks.y,sticks.x, 0));

			target = Vector3.Scale(target, MirrorAxis);
		}
		else
		{
			target = Angle2Vector3s(Vector3.zero , targetRigid.velocity.normalized);

			target = Vector3.Scale(target , MirrorAxis);

//			print(targetRigid.velocity.normalized + " " + target);

		}

		if(target == Vector3.zero && returnToDefault)
		{
			target = defaultRot;
		}


		if(localEulers)
		{
			transform.localEulerAngles = Lerp2Vector3Angles(transform.localEulerAngles, target, lerpSpeed);

		}
		else
		{
			transform.eulerAngles = Lerp2Vector3Angles(transform.eulerAngles, target, lerpSpeed);

		}


	}

	Vector3 Lerp2Vector3Angles(Vector3 parVecFrom, Vector3 parVecTo, float t)
	{
		Vector3 sol = new Vector3();
		sol.x = Mathf.LerpAngle(parVecFrom.x, parVecTo.x, t);
		sol.y = Mathf.LerpAngle(parVecFrom.y, parVecTo.y, t);
		sol.z = Mathf.LerpAngle(parVecFrom.x, parVecTo.z, t);

		return sol;
	}

	Vector3 Angle2Vector3s(Vector3 parPos1, Vector3 parPos2)
	{
		Vector3 sol = new Vector3();

		sol.x = Vector3.Angle(Vector3.Scale(parPos1, new Vector3(1,0,0)),Vector3.Scale(parPos2, new Vector3(1,0,0)));
		sol.y = Vector3.Angle(Vector3.Scale(parPos1, new Vector3(0,1,0)),Vector3.Scale(parPos2, new Vector3(0,1,0)));
		sol.z = Vector3.Angle(Vector3.Scale(parPos1, new Vector3(0,0,1)),Vector3.Scale(parPos2, new Vector3(0,0,1)));

		return sol;
	}
}
