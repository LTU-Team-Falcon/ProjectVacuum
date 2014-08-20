using UnityEngine;
using System.Collections;

public class AsteroidExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.localScale += new Vector3(20,20,20) * Time.deltaTime;
		if(transform.localScale.z > 15)
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider col)
	{

		Rigidbody rigi = col.gameObject.GetComponent<Rigidbody>();

		if(rigi != null)
		{
			rigi.AddExplosionForce(20, transform.position, 10);
		}
	}
}
