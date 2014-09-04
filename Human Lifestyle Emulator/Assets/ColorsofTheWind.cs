using UnityEngine;
using System.Collections;

public class ColorsofTheWind : MonoBehaviour 
{

	public VacuumSucker vacSucker;
	float maxEmmission;
	Camera lilCam;
	RenderTexture rendoTex;

	// Use this for initialization
	void Awake()
	{

	}

	void Start () 
	{
		maxEmmission = gameObject.particleSystem.emissionRate;

/*		rendoTex = new RenderTexture(20,20,30);
		lilCam = gameObject.GetComponentInChildren<Camera>();
		lilCam.targetTexture = rendoTex;
*//*		this.gameObject.renderer.material.mainTexture = rendoTex;
*/	}
	
	// Update is called once per frame
	void LateUpdate () 
	{

/*		gameObject.particleSystem.emissionRate = maxEmmission * vacSucker.suckPow/vacSucker.suckPotential;
		gameObject.particleSystem.startColor = tex2d.GetPixel(0,0);
*/

	}
}
