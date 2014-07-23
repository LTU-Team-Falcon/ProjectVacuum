using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetScored : MonoBehaviour 
{
	public float Size = 0;
	public List<GameObject> children = new List<GameObject>();
	public List<GameObject> proposedChildren = new List<GameObject>();
	public GameManager gamemanager;

	public Score score;
	public GameObject link;
	public float distance = 100000;
//	private float oldDistance = 100000;

	private bool hasRigid = true;
	private bool hasMoved = true;
	// Use this for initialization
	void Start () 
	{
		Invoke("FindChildren" , 0.1f);
		if(rigidbody == null)
		{
			hasRigid = false;
		}
	}

	void FindChildren()
	{

	}

	/*public void findAndScoreLink()
	{
		foreach(GameObject holo in gamemanager.holoList)
		{
			if(holo.gameObject.name == this.gameObject.name)
			{
				Vector3 relativeDistance = this.transform.position - holo.transform.position ;
				float dist2 = relativeDistance.sqrMagnitude;
				if(dist2 < this.distance)
				{

					HoloPositioner holoComp = holo.GetComponent<HoloPositioner>();
					if(dist2 < holoComp.distance)
					{
						print("got Ere");


						if(link!= null)
						{
							link.GetComponent<HoloPositioner>().link = null; //release this object from former tie
							link.GetComponent<HoloPositioner>().distance = 1000000;
						}

						distance = dist2; // shift these values to be relative to new connection
						link = holo;
					}
				}


			}
		}

		if(link!= null)
		{
			HoloPositioner	holoComponent = link.GetComponent<HoloPositioner>();
			
			if(holoComponent.link != null)
			{
				holoComponent.link.hasMoved = true; //make sure new connection's old link is taken care of
				holoComponent.link.distance = 10000;
			}
			
			holoComponent.link = this; //set the new holoposition to us
			holoComponent.distance = distance;
		}
	}*/





	// Update is called once per frame
	void Update () 
	{
		if(hasRigid)
		{
			if(hasMoved && rigidbody.velocity.sqrMagnitude < 0.1)
			{
				//findAndScoreLink();
				hasMoved = false;
			}
			else if(!hasMoved && rigidbody.velocity.sqrMagnitude >= 0.1)
			{
				hasMoved = true;
			}

			if(link != null)
			{
				Debug.DrawLine(transform.position, link.transform.position);
			}

		}
	}


}
