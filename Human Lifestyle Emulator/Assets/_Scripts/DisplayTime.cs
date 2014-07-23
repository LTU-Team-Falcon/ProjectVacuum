using UnityEngine;
using System.Collections;

public class DisplayTime : MonoBehaviour 
{
	private float levelStartTime;
	public int phaseTime = 180;
	public int timeLeft;
	public GUIText TimeText;

	private GameManager gameManager;
	
	
	// Use this for initialization
	void Start () 
	{
		levelStartTime = Time.time;
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeLeft = (int)phaseTime - Mathf.FloorToInt(Time.time - levelStartTime);
		TimeText.text = "Time: " + timeLeft;
		TimeText.color = new Color(1 - (float)timeLeft/phaseTime, (float)timeLeft/phaseTime, 0);
		if(timeLeft < 0)
		{
			TimeText.color = Color.black;
			TimeText.text = "END OF GAME";
		}
	}
}
