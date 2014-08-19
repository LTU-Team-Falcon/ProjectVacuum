using UnityEngine;
using System.Collections;

public class AsteroidDamager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag != "Explosion")
		{
			Transform shadow = transform.FindChild("ShadowProjector");
			Transform splosionTemplate = transform.FindChild("Splosion");
			
			GameObject splosion = Instantiate((Object)transform.FindChild("Splosion").gameObject, col.contacts[0].point, transform.rotation) as GameObject;
			splosion.SetActive(true);
			transform.DetachChildren();
			splosion.transform.localScale = new Vector3(1,1,1);
			shadow.parent = this.transform;
			splosionTemplate.parent = this.transform;
		}

	}
}
