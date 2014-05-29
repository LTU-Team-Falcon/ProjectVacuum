using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {

	public int playerScore = 0;
	public float speedMult = 1;
	
//	[HideInInspector]
	public float idealCharSize;
	public TextMesh texty;

	private VacuumSucker vacSucker;
	// Use this for initialization
	void Start () 
	{
		texty = gameObject.GetComponentInChildren<TextMesh>();
		idealCharSize = texty.characterSize;
		vacSucker = GameObject.FindObjectOfType<VacuumSucker>();
		playerScore = (int)vacSucker.suckPotential;
		if(GameObject.FindObjectOfType<GameManager>().isShootingPhase == true)
		{
			playerScore = PlayerPrefs.GetInt("Score");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		texty.text = "Score : " + (playerScore - 3).ToString();
		texty.text += "    Pow: " + vacSucker.suckPow;
		if(texty.characterSize > idealCharSize) texty.characterSize -= 0.01f;
		if(texty.characterSize < idealCharSize) texty.characterSize += 0.01f;
	}
	
}









