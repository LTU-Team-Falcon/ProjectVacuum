using UnityEngine;
using System.Collections;

public class SwingLegs : MonoBehaviour 
{
	public Transform TransA;
	public Transform TransB;

	Vector2 defaultRotX2;

	public Vector2 minMaxSwingX;
	public Vector2 minMaxSwing2;

	public Vector2 SwingOffsetAxy;
	public Vector2 SwingOffsetBxy;

	public Vector2 lerpSpeed;

	public XinputHandler input;

	public bool secondAxisIsY = false;
	public bool setSticksToMagnitudeOnly = false;
	Vector2 localTime;


	// Use this for initialization
	void Start () 
	{
		if(secondAxisIsY)
		{
			defaultRotX2 = new Vector2(TransA.localEulerAngles.x, TransA.localEulerAngles.y);
		}
		else
		{
			defaultRotX2 = new Vector2(TransA.localEulerAngles.x, TransA.localEulerAngles.z);
		}

		localTime = new Vector2(Time.time, Time.time);
	}

	void OnEnable()
	{
		localTime = new Vector2(Time.time, Time.time);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 stickInput = input.GetLeftStick();
		if(setSticksToMagnitudeOnly)
		{
			stickInput.x = stickInput.magnitude;
			stickInput.y = stickInput.x;
		}
		stickInput.x = Mathf.Clamp(stickInput.x *5,-1,1);
		stickInput.y = Mathf.Clamp(stickInput.y *5,-1,1);

		localTime.x += stickInput.y*Time.deltaTime;
		localTime.y += stickInput.x*Time.deltaTime;	

		stickInput = new Vector2( Mathf.Abs(stickInput.x), Mathf.Abs(stickInput.y));

		if(secondAxisIsY)
		{
			float rotAX = TransA.localEulerAngles.x;
			float rotAY = TransA.localEulerAngles.y;
			
			float lerpTimeX = Mathf.Cos(localTime.x*lerpSpeed.x  + SwingOffsetAxy.x*Mathf.PI) * 0.5f + 0.5f;
			float lerpTimeY = Mathf.Cos(localTime.y*lerpSpeed.y  + SwingOffsetAxy.y*Mathf.PI) * 0.5f + 0.5f;

			rotAX = Mathf.LerpAngle(minMaxSwingX.x, minMaxSwingX.y, lerpTimeX);
			rotAY = Mathf.LerpAngle(minMaxSwing2.x, minMaxSwing2.y, lerpTimeY);

			rotAX = Mathf.LerpAngle(defaultRotX2.x, rotAX, stickInput.y );
			rotAY = Mathf.LerpAngle(defaultRotX2.y, rotAY, stickInput.x );

			TransA.localEulerAngles = new Vector3( rotAX,rotAY, 0);
			
			
			
			float rotBX = TransB.localEulerAngles.x;
			float rotBY = TransB.localEulerAngles.y;


			lerpTimeX = Mathf.Cos(localTime.x*lerpSpeed.x  + SwingOffsetBxy.x*Mathf.PI) * 0.5f + 0.5f;
			lerpTimeY = Mathf.Cos(localTime.y*lerpSpeed.y  + SwingOffsetBxy.y*Mathf.PI) * 0.5f + 0.5f;

			rotBX = Mathf.LerpAngle(minMaxSwingX.x, minMaxSwingX.y, lerpTimeX);
			rotBY = Mathf.LerpAngle(minMaxSwing2.x, minMaxSwing2.y, lerpTimeY);
	
			rotBX = Mathf.LerpAngle(defaultRotX2.x, rotBX, stickInput.y );
			rotBY = Mathf.LerpAngle(defaultRotX2.y, rotBY, stickInput.x );

			TransB.localEulerAngles = new Vector3( rotBX, rotBY, 0);
		}
		else
		{
			float rotAX = TransA.localEulerAngles.x;
			float rotAZ = TransA.localEulerAngles.z;
			
			float lerpTimeX = Mathf.Cos( localTime.x * lerpSpeed.x  + SwingOffsetAxy.x*Mathf.PI)* 0.5f + 0.5f;
			float lerpTimeZ = Mathf.Cos( localTime.y * lerpSpeed.y  + SwingOffsetAxy.y*Mathf.PI)* 0.5f + 0.5f;

			rotAX = Mathf.LerpAngle(minMaxSwingX.x, minMaxSwingX.y, lerpTimeX);
			rotAZ = Mathf.LerpAngle(minMaxSwing2.x, minMaxSwing2.y, lerpTimeZ);

			rotAX = Mathf.LerpAngle(defaultRotX2.x, rotAX, stickInput.y);
			rotAZ = Mathf.LerpAngle(defaultRotX2.y, rotAZ, stickInput.x);

			TransA.localEulerAngles = new Vector3( rotAX, 0, rotAZ);

			float rotBX = TransB.localEulerAngles.x;
			float rotBZ = TransB.localEulerAngles.z;
			
			lerpTimeX = Mathf.Cos( localTime.x * lerpSpeed.x  + lerpSpeed.x *  Mathf.PI ) * 0.5f + 0.5f;
			lerpTimeZ = Mathf.Cos( localTime.y * lerpSpeed.y  + lerpSpeed.y *  Mathf.PI ) * 0.5f + 0.5f;

			rotBX = Mathf.LerpAngle( minMaxSwingX.x , minMaxSwingX.y , lerpTimeX );
			rotBZ = Mathf.LerpAngle( minMaxSwing2.x , minMaxSwing2.y , lerpTimeZ );

			rotBX = Mathf.LerpAngle(defaultRotX2.x, rotBX, stickInput.y );
			rotBZ = Mathf.LerpAngle(defaultRotX2.y, rotBZ, stickInput.x );

			TransB.localEulerAngles = new Vector3( rotBX , 0 , rotBZ);
		}



	}
}
