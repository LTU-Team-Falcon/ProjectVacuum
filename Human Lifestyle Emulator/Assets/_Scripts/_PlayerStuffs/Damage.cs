using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	
	public bool IsDead;

	private float damageCounter;
	private int Lives;
	private int RespawnCount;
	public GUIText DamageText;

	public  GameObject VacuumObject;


	// Use this for initialization
	void Start () {
		
		damageCounter = 0;
		Lives = 3;
		IsDead = false;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.y < -15)
		{
			IsDead = true;
			OnDeath();
		}

		if (IsDead == true)
		{
			RespawnCount++;
		}

		if (RespawnCount == 150)
		{
			OnSpawn();
		}

		if (Lives == 0)
		{
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
			float Factor = col.rigidbody.mass * col.relativeVelocity.magnitude * damageCounter * 750;
			rigidbody.AddForce(direction.normalized * Factor);
			damageCounter += col.rigidbody.mass * 5;
		}
	}

	void OnDeath ()
	{
		transform.renderer.enabled = false;
		transform.position = new Vector3(0,20,0);
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		VacuumObject.renderer.enabled = false;
		Lives--;
	}

	void OnSpawn ()
	{
		transform.renderer.enabled = true;
		VacuumObject.renderer.enabled = true;
		IsDead = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		RespawnCount = 0;
	}
}