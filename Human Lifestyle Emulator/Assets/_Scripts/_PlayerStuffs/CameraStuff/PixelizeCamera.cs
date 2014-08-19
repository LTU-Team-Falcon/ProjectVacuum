using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PixelizeCamera : MonoBehaviour 
{
	public int offset = 0;
	public int antiAlias = 2;
	public int numLOD;
	public float minResPcent;
	public float maxResPcent;

	public float closeRad;
	public float farRad;

	public float _SpreadRad = 0;
	float spreadRad;
	public float _SpreadRes = 0;
	float spreadRes;

	public bool shouldUpdate = false;
	// Use this for initialization


	List<GameObject> addedThings = new List<GameObject>();
	List<RenderTexture> addedTexs = new List<RenderTexture>();


	bool isUniversal = false;

	public List<PixelizeCamera> pixy = new List<PixelizeCamera>();
	void Awake()
	{
		if(gameObject.camera == null)
		{
			pixy.AddRange(GameObject.FindObjectsOfType<PixelizeCamera>() );
			isUniversal = true;
			pixy.Remove(this);
			shouldUpdate = true;
		}
	}



	void Start()
	{
		Init();
	}

	void Init () 
	{
		spreadRad = SpreadTheNumbers(_SpreadRad);

		spreadRes = SpreadTheNumbers(_SpreadRes);

		RenderTexture rendOtex;
		Rect rectjob = this.camera.pixelRect;
		Vector2 rendDims;

		for(int i = 1; i < (numLOD + 1); i++)
		{
			float lastRadi = getOuterRadius(i -1);
			float thisRadi = getOuterRadius(i);
			float resPercent = getResolutionPercentage(i);

			string camName = "ZonedCamera";
			if(i == numLOD)
				camName += "Finale";

			GameObject camGam = GameObject.Instantiate(Resources.Load(camName), transform.position, transform.rotation) as GameObject;
			camGam.transform.parent = this.transform;
			camGam.name = (string) (camName + " - " + i);

			camGam.camera.pixelRect = rectjob;

			camGam.camera.nearClipPlane = lastRadi - 2;
			camGam.camera.farClipPlane = thisRadi + 2;

			bool slideFlag = false;

			if(i == numLOD && gameObject.GetComponent<Skybox>() != null)
			{
				Skybox skyboxFurRelz = gameObject.GetComponent<Skybox>();

				if(skyboxFurRelz.enabled)
				{
					slideFlag = true;
				}
				else
				{
					Skybox newkid = camGam.AddComponent<Skybox>();
					newkid.material = skyboxFurRelz.material;
				}
			}




			rendDims = new Vector2((int)(rectjob.width*resPercent),(int)(rectjob.height*resPercent));
			if(rendDims.x == 0)
			{
				rendDims += new Vector2(1,0);
			}
			if(rendDims.y == 0)
			{
				rendDims += new Vector2(0,1);
			}

			rendOtex = new RenderTexture((int)(rendDims.x), (int)(rendDims.y), (int)thisRadi);
			rendOtex.filterMode = FilterMode.Point;
			rendOtex.antiAliasing = antiAlias;
			camGam.camera.targetTexture = rendOtex;
			rendOtex.name = (string) ("rendOtex - " + i);




			string slideName;
			if(i != numLOD || slideFlag)
				slideName = "MidViewPlane";
			else
				slideName = "FinaleViewPlane";

			GameObject renderSlide = GameObject.Instantiate(Resources.Load(slideName), transform.position, transform.rotation) as GameObject;
			renderSlide.layer = offset + 27;
			renderSlide.renderer.material.mainTexture = rendOtex;
			renderSlide.name = (string) (slideName + " - " + i);
			renderSlide.transform.parent = this.transform;

			if(i != numLOD)
				renderSlide.transform.localPosition += new Vector3(0,0, thisRadi);
			else
				renderSlide.transform.localPosition += new Vector3(0,0, lastRadi + 5);

			renderSlide.transform.localScale = Vector3.Scale(new Vector3(this.camera.aspect,1,1),renderSlide.transform.localScale);


			addedThings.Add(renderSlide);
			addedThings.Add(camGam);
			addedTexs.Add(rendOtex);
		}

			this.camera.isOrthoGraphic = true;
			this.camera.cullingMask = (1 << (27 + offset));
			this.camera.orthographicSize = 5f;
	}

	float getOuterRadius(int par1Int)
	{
		if(par1Int == numLOD)
		{
			return 5000;
		}
		else if(par1Int == 0)
		{
			return this.camera.nearClipPlane + 2;
		}

		float radPercent = (float)((float)par1Int/(float)numLOD);
		return (float)(farRad - closeRad) * Mathf.Pow(radPercent, spreadRad) + closeRad;
	}


	float getResolutionPercentage(int par1Int)
	{
		float resPercent = (float)(par1Int - 1)/(float)(numLOD - 1);
		return (float)(maxResPcent - minResPcent) * Mathf.Pow(resPercent, spreadRes) + minResPcent;
	}


	float SpreadTheNumbers(float par1float)
	{
		float origSpread = par1float * -1;
		origSpread /= 20;
		if(origSpread >= 0)
		{
			origSpread += 1;
		}
		else
		{
			origSpread = Mathf.Abs(origSpread) + 1;
			origSpread = 1/origSpread;
		}

		return origSpread;
	}





	// Update is called once per frame
	void Update () 
	{
		if(shouldUpdate && isUniversal)
		{
			shouldUpdate = false;
			foreach(PixelizeCamera perx in pixy)
			{
				perx.minResPcent = this.minResPcent;
				perx.maxResPcent = this.maxResPcent;
				perx.farRad = this.farRad;
				perx.closeRad = this.closeRad;
				perx._SpreadRad = this._SpreadRad;
				perx._SpreadRes = this._SpreadRes;
				perx.numLOD = this.numLOD;
				perx.shouldUpdate = true;
			}
		}
		else if(shouldUpdate)
		{
			foreach(GameObject gamObj in addedThings)
				Destroy((Object)gamObj);
			foreach(RenderTexture renTex in addedTexs)
				Destroy((Object)renTex);

			Init();
			shouldUpdate = false;
		}
	}
}
