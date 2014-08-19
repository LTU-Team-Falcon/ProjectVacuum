using UnityEngine;
using System.Collections;

public class DamageOverTime : MonoBehaviour 
{
	public float duration = 0.0f;
	public float delay = 5f;
	public float damagePerTick = 4f;

	private Damage damage;
	private float dawn;

	// Use this for initialization
	void Start () 
	{
		dawn = Time.time;
		damage = gameObject.GetComponent<Damage>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time - dawn > delay)
		{
			if(duration != 0)
			{
				if((Time.time - (dawn + delay) )> duration)
				{
					Destroy(gameObject);
				}
			}

			damage.damageCounter += damagePerTick*Time.deltaTime;
		}
	}
}
