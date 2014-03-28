using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour 
{
	public GameObject[] target;
	public float rotateDistance;
	public float startSpd;
	public float forceMod;
	
	// Use this for initialization
	void Start () 
	{		

			Vector3 relDist = new Vector3(1,1,1)*startSpd; //target[i].transform.position - transform.position;
			
			this.rigidbody.velocity = relDist;
		
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 newForce = new Vector3(0,0,0);
			
		for(int i = 0; i < target.Length; i++)
		{
			Vector3 relDist = transform.position - target[i].transform.position;//target[i].transform.position - transform.position;
			

			float difMag = (rotateDistance - relDist.magnitude)/rotateDistance;
	
			relDist = relDist.normalized;
			
			relDist *= target[i].rigidbody.mass*difMag*forceMod;
			

			rigidbody.AddForce(relDist);
		}
		
	}
}
