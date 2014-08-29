using UnityEngine;

public class LightBehavior : MonoBehaviour {
	
	private new Light light;
	private int curFluxInd = 0;
	
	public bool flashing;
	public float flashEvery = 1.0F;
	public float flashDelay = 0.0F;
	public bool colorFlux;
	public Color[] colors;
	public float fluxEvery = 0.5F;
	public float fluxDelay = 0.0F;
	
	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
		
		if (flashing) {
			InvokeRepeating("FlashLight", flashDelay, flashEvery);
		}
		
		if (colorFlux) {
			InvokeRepeating("ColorFlux", fluxDelay, fluxEvery);
		}
	}
	
	void FlashLight() {
		light.enabled = !light.enabled;
	}
	
	void ColorFlux () {
		curFluxInd++;
		if (curFluxInd >= colors.Length) curFluxInd = 0;
		
		//Set the color to the new color.
		light.color = colors[curFluxInd];
	}
}