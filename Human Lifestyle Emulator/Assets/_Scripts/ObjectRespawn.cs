using UnityEngine;
using System.Collections;

public class ObjectRespawn : MonoBehaviour {

	public Vector3 OrigPos;
	public GameObject Floor;

	private int count;
	private bool IsGone;

	private int Inum = 60;
	// Use this for initialization
	void Start () 
	{
		Floor = GameObject.Find("GroundLayer");
		OrigPos = transform.position;
		Inum -= (int)(Random.value*50);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Inum --;
		if(Inum == 0)
		{
			Inum = 60;
			if (transform.position.y < Floor.transform.position.y-15)
			{
				rigidbody.constraints = RigidbodyConstraints.FreezeAll;
				renderer.enabled = false;
				IsGone = true;
			}
			
			if (IsGone == true) 
			{
				count++;
			}
			
			if (count == 2000) 
			{
				renderer.enabled = true;
				rigidbody.constraints = RigidbodyConstraints.None;
				transform.position = OrigPos;
				count = 0;
			}
		}
	}
}
