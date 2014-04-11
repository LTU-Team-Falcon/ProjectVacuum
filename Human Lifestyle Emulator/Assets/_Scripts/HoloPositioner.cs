using UnityEngine;
using System.Collections;

public class HoloPositioner : MonoBehaviour 
{
	GameManager gameManager;
	public float size;

	public bool hasBeenScored = false;
	public float distance = 10000000;
	public GetScored link;
	// Use this for initialization
	void Start () 
	{
		if(gameObject.renderer != null)
		{
			gameObject.renderer.material.shader = Shader.Find("Custom/InvertedHolo");
			Color goodColor = 1.5f*Vector4.Normalize((Vector4)(renderer.material.color + new Color(0,0,0.5f)));
			gameObject.renderer.material.color = new Color(goodColor.r, goodColor.g,goodColor.b, 0.2f);
			gameObject.renderer.material.SetFloat("_SizeMod", size);
		}

		if(gameObject.collider)
		{
			Destroy(collider);
		}
		Destroy(rigidbody);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
