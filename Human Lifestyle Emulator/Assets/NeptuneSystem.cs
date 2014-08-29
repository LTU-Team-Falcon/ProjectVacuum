using UnityEngine;
using System.Collections;

public class NeptuneSystem : MonoBehaviour 
{

	float[] oldSenses = new float[8];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnWarpIn()
	{
		MouseForce[] sense = GameObject.FindObjectsOfType<MouseForce>();
		for(int i =0; i < sense.Length; i++)
		{
			oldSenses[i*2] = sense[i].sensitivityX;
			oldSenses[i*2 + 1] = sense[i].sensitivityY;

			sense[i].sensitivityY = 70;
			sense[i].sensitivityX = 70;
		}

		Physics.gravity = 9.81f * new Vector3(0, -0.1f, 0);
	}

	public void OnWarpOut()
	{
		MouseForce[] sense = GameObject.FindObjectsOfType<MouseForce>();
		for(int i =0; i < sense.Length; i++)
		{
			sense[i].sensitivityX =  oldSenses[i*2];
			sense[i].sensitivityY = oldSenses[i*2 + 1];
		}
		Physics.gravity = 9.81f * new Vector3(0, -1, 0);

	}
}
