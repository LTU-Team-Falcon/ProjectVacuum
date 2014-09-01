using UnityEngine;
using System.Collections;

public class ObjectRespawn : MonoBehaviour {

	public Vector3 OrigPos;

	private GameObject Floor;

	public int count;
	public bool IsGone;

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
			
			if (count == 60) 
			{
				renderer.enabled = true;
				collider.enabled = true;
				rigidbody.constraints = RigidbodyConstraints.None;
				transform.position = OrigPos;
				count = 0;
			}


		}

		if (gameObject.tag == "PowerUP" && collider.enabled == false)
		{
			Debug.Log("Gone");
			IsGone = true;
			count = 20;
			foreach( Transform child in transform )
			{
				child.gameObject.SetActive(false);
			}

		}
	}
}
