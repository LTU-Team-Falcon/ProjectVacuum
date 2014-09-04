using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class OCDReorder : MonoBehaviour 
{
	public int newIndex;
	public bool run = false;
	public bool destroyafterrun = false;
	public int currentIndex;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentIndex = transform.GetSiblingIndex() + 1;
		if(run)
		{
			run = false;
			transform.SetSiblingIndex(newIndex - 1);
			if(destroyafterrun)
			{
				Destroy(this);
			}
		}


	}
}
