using UnityEngine;
using System.Collections;

public class ScoreParticle : MonoBehaviour 
{
	Score score;
	public int value;
	private TextMesh textMesh;
	private float startCharSize;
	
	public float life = 2f;
	
	public Vector3 pseudoVelocity = new Vector3(Random.value*4f - 2,0, Random.value*4f -2);
	// Use this for initialization
	void Start () 
	{
		 score = GameObject.FindObjectOfType<Score>();
		 textMesh = gameObject.GetComponentInChildren<TextMesh>();
		 textMesh.text = "+" + value.ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
		life -= Time.deltaTime;
		if(life <= 0) {		DoTheDirtyBusiness(GameObject.FindGameObjectWithTag("Scorcerer"));	}
		Vector3 targetPos = score.transform.position;
		
		Vector3 relPos =  targetPos - transform.position;
				
		relPos *= score.speedMult;
		
		pseudoVelocity += relPos*Time.deltaTime;
		
		transform.position += pseudoVelocity*Time.deltaTime;
	}
	
	void DoTheDirtyBusiness(GameObject scorcerer)
	{
		scorcerer.GetComponent<Score>().playerScore += this.value;
		scorcerer.GetComponent<Score>().texty.characterSize = 0.3f;
		Destroy(this.gameObject);
		Destroy(this);
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.GetComponent<Score>() != null)
		{
			DoTheDirtyBusiness(col.gameObject);
		}
	}
}
