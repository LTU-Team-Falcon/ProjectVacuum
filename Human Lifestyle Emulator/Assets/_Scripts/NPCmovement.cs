using UnityEngine;
using System.Collections;

public class NPCmovement : MonoBehaviour {
	public Transform start;
	public Transform Point2;
	public Transform Point3;
	public Transform Point4;
	public Transform Point5;
	public Transform Point6;
	public Transform Point7;


	// Use this for initialization
	void Start () {

		transform.position = start.position;

	}
	
	// Update is called once per frame
	void Update () {
	

		if (transform.position == start.transform.position)
		{
			//toPoint();
		}


	}

	void FixedUpdate()
	{
		while (transform.position != Point2.position)
		{
			transform.position = Vector3.MoveTowards(transform.position, Point2.position ,.1f);
			Debug.Log("MOVE");
		}

	}

}
