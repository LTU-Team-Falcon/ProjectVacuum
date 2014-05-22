using UnityEngine;
using System.Collections;

public class ScoreParticle : MonoBehaviour 
{
	public int value;
	private TextMesh textMesh;

	public float life = 2f;
	// Use this for initialization
	void Start () 
	{
		textMesh = gameObject.GetComponentInChildren<TextMesh>();
		textMesh.text = "+" + value.ToString();
		this.rigidbody.velocity = new Vector3(Random.value*2f - 1,Random.value*3+2, Random.value*2f - 1);
	}
	
	// Update is called once per frame
	void Update () 
	{
		life -= Time.deltaTime;
		if(life <= 0) 
		{	
			Destroy(this.gameObject);
			Destroy(this);
		}
	}
}
