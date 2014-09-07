using UnityEngine;
using System.Collections;

public class GUIPlacement : MonoBehaviour {

	public Transform P1UI;
	public Transform P2UI;
	public Transform P3UI;
	public Transform P4UI;
	public bool TopLeft;
	public bool BottomRight;
	public bool Middle;
	public bool TopRight;

	// Use this for initialization
	void Start () {
		P1UI = transform.Find ("P1");
		P2UI = transform.Find ("P2");
		P3UI = transform.Find ("P3");
		P4UI = transform.Find ("P4");

		if (BottomRight == true)
		{
			
			P1UI.transform.localPosition = new Vector3(0,-.27f,0);
			P2UI.transform.localPosition = new Vector3(-.35f,-.27f,0);
			P4UI.transform.localPosition = new Vector3(-.35f,0,0);
		}
		if (Middle == true)
		{
			P1UI.transform.localPosition = new Vector3(.35f,-.27f,0);
			P2UI.transform.localPosition = new Vector3(-.35f,-.27f,0);
			P3UI.transform.localPosition = new Vector3(.35f,.27f,0);
			P4UI.transform.localPosition = new Vector3(-.35f,.27f,0);
		}
		if (TopRight == true)
		{
			P1UI.transform.localPosition = new Vector3(.35f,0,0);
			P3UI.transform.localPosition = new Vector3(.35f,.27f,0);
			P4UI.transform.localPosition = new Vector3(0,.27f,0);
		}
		if (TopLeft == true)
		{
			P2UI.transform.localPosition = new Vector3(-.35f,0,0);
			P3UI.transform.localPosition = new Vector3(0,.27f,0);
			P4UI.transform.localPosition = new Vector3(-.35f,.27f,0);
		}


	}
	
	// Update is called once per frame
	void Update () {
	



	}
}
