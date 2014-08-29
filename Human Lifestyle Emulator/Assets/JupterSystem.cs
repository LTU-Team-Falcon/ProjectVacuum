using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JupterSystem : MonoBehaviour 
{
	public Vector2 minMax = new Vector2(1111,2632);

	GameObject jupiterPrime;
	List<Transform> playerContainers = new List<Transform>();
	// Use this for initialization
	void Start () 
	{
		jupiterPrime = transform.FindChild("JupiterRotation").FindChild("Jupiter").gameObject;

/*		GameObject[] playObjs = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject playaObj in playObjs)
		{
			playerContainers.Add(playaObj.transform);
		}*/

		for(int i = 0; i < 4; i++)
		{
			playerContainers.Add( GameObject.Find("PlayerContainer" + (i+1)).transform );
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 jupPos = jupiterPrime.transform.position;
		Vector3 jupiterAngle = jupPos.normalized;
		Vector3 gravityAngle = Vector3.Lerp( new Vector3(0,-1,0) , jupiterAngle , ((jupPos.magnitude - minMax.x)/(minMax.y - minMax.x))*0.1f + 0.33f );

		Physics.gravity = 9.81f * gravityAngle;

/*		foreach(Transform playerCont in playerContainers)
		{

/*			Transform handCube = playerCont.FindChild("HandCube");
			Transform maincam = handCube.FindChild("Main Camera");
			maincam.LookAt( maincam.forward , maincam.position + -10 * gravityAngle );
*/			/*			Transform actualPlayer = playerCont.FindChild("Player");
			Transform handCube = playerCont.FindChild("HandCube");

			Vector3 origRot = actualPlayer.eulerAngles;
			actualPlayer.LookAt( handCube.forward , actualPlayer.position + -10 * gravityAngle );
			Vector3 difference = actualPlayer.eulerAngles - origRot;
			print(difference);
			//playerCont.FindChild("HandCube").RotateAround(actualPlayer.position, vector3) += difference;
			//playerCont.FindChild("VacuumObject").eulerAngles += difference;
		}*/
	}


	public void OnWarpOut()
	{
		Physics.gravity = 9.81f * new Vector3(0,-1,0);
	}

	public void OnWarpIn()
	{

	}
}
