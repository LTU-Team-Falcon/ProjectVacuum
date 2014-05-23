using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCMovement : MonoBehaviour {

	public List<Transform> NodesToMove = new List<Transform>();

	private SphereCollider col;
	public int count;
	private Transform NextNode;



	void Awake()
	{
		col = GetComponent<SphereCollider>();
		count = 0;
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (count == 100)
		{
			int num = NodesToMove.Count;
			int node = Random.Range(0, num);

			for (int i = 0; i<num;i++)
			{
				Vector3 direction = NodesToMove[i].transform.position - transform.position;
				RaycastHit hit;
				
				if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius))
				{
					if ( hit.collider.gameObject.tag != "Node")
					{
						NodesToMove.RemoveAt(i);
					}

				}
			}
			NextNode = NodesToMove[node];	
		}
	
		if (count > 100)
			Move();

		if (count == 400)
			count = 0;

		count++;
	}
	
	void Move ()
	{
		transform.position = Vector3.MoveTowards(transform.position,NextNode.transform.position,.05f);
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Node")
		{
			Vector3 direction = other.transform.position - transform.position;
			RaycastHit hit;

			if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius))
			{
				if ( hit.collider.gameObject.tag == "Node")
				{
					if (NodesToMove.Contains(other.transform))
					{
				
					}
					else
					{
						NodesToMove.Add(other.transform);
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Node")
		{
			NodesToMove.Remove(other.transform);
		}
	}



}
