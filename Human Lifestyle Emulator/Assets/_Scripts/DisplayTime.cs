using UnityEngine;
using System.Collections;

public class DisplayTime : MonoBehaviour 
{

	private float levelStartTime;
	public int phaseTime = 180;
	public TextMesh texty;

	private GameManager gameManager;
	
	
	// Use this for initialization
	void Start () 
	{
		levelStartTime = Time.time;
		texty = gameObject.GetComponent<TextMesh>();
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		int timeLeft = (int)phaseTime - Mathf.FloorToInt(Time.time - levelStartTime);
		texty.text = "Time: " + timeLeft;
		texty.color = new Color(1 - (float)timeLeft/levelStartTime, (float)timeLeft/levelStartTime, 0);
		if(timeLeft < 0)
		{
			gameManager.EndSucking();
		}
	}
}
