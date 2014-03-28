using UnityEngine;
using System.Collections;

public class DisplayTime : MonoBehaviour 
{

	private float levelStartTime;
	public int firstPhaseTime = 180;
	public int secondPhaseTime = 60;
	public TextMesh texty;
	
	
	// Use this for initialization
	void Start () 
	{
		levelStartTime = Time.time;
		texty = gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		int timeLeft = (int)firstPhaseTime - Mathf.FloorToInt(Time.time - levelStartTime);
		texty.text = "Time: " + timeLeft;
		texty.color = new Color(1 - (float)timeLeft/levelStartTime, (float)timeLeft/levelStartTime, 0);
		if(timeLeft < 0)
		{
			GameObject.FindObjectOfType<VacuumSucker>().isSucking = false;
			Score score = GameObject.FindObjectOfType<Score>();
			score.transform.localPosition = new Vector3(score.transform.localPosition.x,0,score.transform.localPosition.z);
			score.texty.characterSize = 0.3f;
			score.texty.text = "Score : " + score.playerScore;
			GameObject.FindObjectOfType<GameManager>().ForcePause();
		}
	}
}
