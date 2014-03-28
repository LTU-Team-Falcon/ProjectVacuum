using UnityEngine;
using System.Collections;

public class ScoreParticle : MonoBehaviour 
{
	Score score;
	public int value;
	private TextMesh textMesh;
	private float startCharSize;
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
		
		Vector3 targetPos = score.transform.position;
		
		Vector3 relPos =  targetPos - transform.position;
				
		relPos *= score.speedMult;
		
		transform.position += relPos*Time.deltaTime;
		
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.GetComponent<Score>() != null)
		{
			col.gameObject.GetComponent<Score>().playerScore += this.value;
			col.gameObject.GetComponent<Score>().texty.characterSize = 0.3f;
			Destroy(this.gameObject);
			Destroy(this);
		}
	}
}
