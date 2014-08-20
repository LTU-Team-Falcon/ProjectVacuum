using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Damage : MonoBehaviour {

	//[HideInInspector]
	public float damageCounter;

	public int Lives;
	[HideInInspector]
	public bool IsDead;
	//[HideInInspector]
	public int RespawnCount;
	[HideInInspector]
	public GUIText DamageText;
	[HideInInspector]
	public List<GameObject> Respawns = new List<GameObject>();

	[HideInInspector]
	public bool BoutToDie = false;
	public float DeathTimer;




	// Use this for initialization
	void Start () {

		if(DamageText == null)
		{
			string name = "P" + (int)(transform.parent.gameObject.GetComponent<XinputHandler>().indexNum +1) + "UI";
			DamageText = GameObject.Find(name).guiText;
		}
		damageCounter = 0;
		IsDead = false;
		Lives = 3;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (IsDead == true)
		{
			RespawnCount++;
		}

		if (RespawnCount == 50)
		{
			OnSpawn();
		}

/*		if (Vector3.Distance (transform.position, new Vector3 (0, 0, 0)) > 200) 
		{
			IsDead = true;
			OnDeath();
		}
*/
		/*if(BoutToDie)
		{
			print("DeathTimer" + DeathTimer);
			DeathTimer --;
		}
		if(BoutToDie && DeathTimer <= 0)
		{
			IsDead = true;
			OnDeath();
		}*/

		if (Lives == 0)
		{
			string DeathCamName = "P" + (int)(transform.parent.gameObject.GetComponent<XinputHandler>().indexNum +1) + "DeathCam";
			GameObject DeathCam =  GameObject.Find(DeathCamName);
			DeathCam.camera.enabled = true;
			transform.parent.gameObject.SetActive(false);
		}

		DamageText.text = damageCounter + "%";

	}
	
	
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Fired")
		{
			col.gameObject.tag = "Suckable";
			Vector3 direction =  transform.position - col.transform.position;
			float Factor = col.rigidbody.mass * col.relativeVelocity.magnitude * damageCounter * 50;
			rigidbody.AddForce(direction.normalized * Factor);
			damageCounter += col.rigidbody.mass * 5;
		}
	}

	void OnDeath ()
	{
		foreach (GameObject respawn in GameObject.FindGameObjectsWithTag("Respawn Points"))
		{
			Respawns.Add(respawn);
		}

		foreach(Transform brother in transform.parent)
		{
		//	brother.position = Respawns [Random.Range(0,Respawns.Count)].transform.position;
			brother.gameObject.rigidbody.velocity = new Vector3 (0, 0, 0);

			brother.gameObject.rigidbody.isKinematic = true;
		}

		//transform.position = Respawns [Random.Range(0,Respawns.Count)].transform.position;
		//BoutToDie = false;

		Lives--;
	}

	void OnSpawn ()
	{
		//get distance for all players from spawn points and choose the farthest one
		

		foreach(Transform brother in transform.parent)
		{
			brother.gameObject.rigidbody.isKinematic = false;
			brother.position = Respawns [Random.Range(0,Respawns.Count)].transform.position;
			//brother.gameObject.rigidbody.velocity = new Vector3 (0, 0, 0);

		}

		IsDead = false;
		//rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		RespawnCount = 0;
	}

	


	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.name == "KillBox") 
		{
			IsDead = true;
			OnDeath ();
		}

		if (col.gameObject.name == "Respawn Point") 
		{
			col.gameObject.tag = "Untagged";
		}

		/*if(col.gameObject.tag == "SafeZone")
		{
			//print("im in.");
			BoutToDie = false;
		}*/
	}

	void OnTriggerExit (Collider col)
	{
		if (col.gameObject.name == "Respawns Point") 
		{
			col.gameObject.tag = "RespawnPoints";
		}
		/*else
		if(col.gameObject.tag == "SafeZone")
		{
			print("IM OUT BE WARY!!");
			DeathTimer = 5 * 60f;
			BoutToDie = true;
		}*/
	}
	
}