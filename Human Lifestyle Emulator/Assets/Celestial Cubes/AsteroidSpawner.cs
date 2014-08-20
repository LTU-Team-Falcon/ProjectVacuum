using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour 
{
	public Vector2 OffsetX;
	public Vector2 OffsetY;
	public Vector2 OffsetZ;

	public int timer = 60;
	int iNum = 0;

	public Vector2 SizeOffset;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		iNum --;

		if(iNum <= 0)
		{

			Invoke("SpawnAnAsteroid", 1f);



			iNum = timer;
		}
	}

	void SpawnAnAsteroid()
	{
		float offX = Random.value * (OffsetX.y - OffsetX.x) + OffsetX.x;

		float offY = Random.value * (OffsetY.y - OffsetY.x) + OffsetY.x;

		float offZ = Random.value * (OffsetZ.y - OffsetZ.x) + OffsetZ.x;

		GameObject newAsteroid = (GameObject )Instantiate( Resources.Load("Asteroid"));
		newAsteroid.transform.position = this.transform.position + new Vector3(offX, offY, offZ);

		float newSize = Random.value;
		newAsteroid.rigidbody.mass = (int)(500 * newSize) + 100;
		newSize *= (SizeOffset.y - SizeOffset.x) + SizeOffset.x;

		newAsteroid.transform.localScale = new Vector3(newSize,newSize, newSize);

		newAsteroid.GetComponentInChildren<Projector>().orthographicSize = newSize;
	}
}
