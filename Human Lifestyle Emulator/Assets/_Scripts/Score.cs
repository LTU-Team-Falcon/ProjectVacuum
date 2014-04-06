using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour 
{
	public int playerScore = 0;
	public float speedMult = 1;
	
//	[HideInInspector]
	public float idealCharSize;
	public TextMesh texty;
	// Use this for initialization
	void Start () 
	{
		texty = gameObject.GetComponentInChildren<TextMesh>();
		idealCharSize = texty.characterSize;
		playerScore = (int)GameObject.FindObjectOfType<VacuumSucker>().suckPotential;
		if(GameObject.FindObjectOfType<GameManager>().shootingPhase == true)
		{
			playerScore = PlayerPrefs.GetInt("Score");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		texty.text = "Power : " + playerScore.ToString();
		if(texty.characterSize > idealCharSize) texty.characterSize -= 0.01f;
		if(texty.characterSize < idealCharSize) texty.characterSize += 0.01f;
	}
}
