using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class WarpNotification : MonoBehaviour 
{
	float birthTime;

	Color color1 = Color.white;
	Color color2 = Color.red;
	bool doesLerpColors = false;
	Vector2 flashMod = new Vector2(1,1);
	// Use this for initialization
	void OnEnable () 
	{
		//int numPlayers = GameObject.FindObjectOfType<GameManager>().getNumberOfPlayers();


		Init(0.5f, 0.5f);
	}

	void Start()
	{
		birthTime = Time.time;
	}


	void Init(float relPosX, float relPosY)
	{
		guiTexture.pixelInset = new Rect(relPosX * Screen.width , relPosY * Screen.height ,1,1);
	}

	void Init(float relPosX, float relPosY, float LifeTime)
	{
		guiTexture.pixelInset = new Rect(relPosX * Screen.width , relPosY * Screen.height ,1,1);
		Destroy(this.gameObject, LifeTime);

	}


	public void SetColorStuff(Color parColor1, Color parColor2, Vector2 parflashMod)
	{
		doesLerpColors = true;
		color1 = parColor1;
		color2 = parColor2;
		flashMod = parflashMod;
	}



	/*void Init(float relPosX, float relPosY, int LifeTime, Color parColor1, Color parColor2, Vector2 parflashMod)
	{
		guiTexture.pixelInset = new Rect(relPosX * Screen.width , relPosY * Screen.height ,1,1);

	}*/

	// Update is called once per frame
	void Update () 
	{

		if(doesLerpColors)
		{
			guiTexture.color = Color.Lerp(color1, color2,  flashMod.y *(Mathf.Cos( (Time.time - birthTime) * flashMod.x )* 0.5f + 0.5f) );
		}
	}
}
