using UnityEngine;
using System.Collections;

public class GetSucked : MonoBehaviour 
{
	public bool isDebugging = false;
	TextMesh debugTextMesh;
	
	public Vector3 origSize;
	
	private VacuumSucker vacuumSucker;
	private Vector3 velocity;
	private Vector3 force;
	
	public bool canGetSucked = true;
	public float resistance = 0f;
	public float health = 0;
	public float size = 0f;

	// Use this for initialization
	void Start () 
	{
		origSize = transform.localScale;
		if(!(size > 0)) size = rigidbody.mass;//temp
		if(!(health > 0)) health = (int)size;
		if(!(resistance > 0)) resistance = health;
		
		vacuumSucker = GameObject.FindGameObjectWithTag("Vacuum").GetComponent("VacuumSucker") as VacuumSucker;
		gameObject.tag = "Suckable";
		
		
		if(this.isDebugging)
		{
			GameObject textObj = vacuumSucker.playerObj.GetComponentInChildren<GameManager>().createNewDebugTextObj();
			textObj.transform.position = this.transform.position + Vector3.up * 1.5f;
			textObj.transform.parent = this.transform;

			debugTextMesh = textObj.GetComponent<TextMesh>();
		}
		

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(canGetSucked)
		{	
			Vector3 relVec = rigidbody.ClosestPointOnBounds(vacuumSucker.transform.position);
			relVec = relVec - vacuumSucker.transform.position;//new Vector3(transform.position.x - vacuumSucker.transform.position.x, transform.position.y - vacuumSucker.gameObject.transform.position.y, transform.position.z - vacuumSucker.gameObject.transform.position.z);
			
			float relDist2 = relVec.sqrMagnitude;
			if(relDist2 < vacuumSucker.suckDist2)
			{
				
				force = relVec.normalized*vacuumSucker.suckPow;
				force *= (vacuumSucker.suckDist2 - relDist2)/vacuumSucker.suckDist2;
				force *= Time.deltaTime*-20;
				rigidbody.AddForceAtPosition(force,relVec + vacuumSucker.transform.position);
				
				if(force.magnitude >= resistance*0.3f)
				{
					UnLockRigid();
				}
			}
		}
	}
	
	void LateUpdate()
	{

	}
	
	public void LockRigid()
	{
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		rigidbody.isKinematic = true;
	}
	
	public void UnLockRigid()
	{
		rigidbody.constraints = RigidbodyConstraints.None;
		rigidbody.isKinematic = false;
	}
	
	
	
	void OnGUI()
	{
		if(isDebugging)	{	debugTextMesh.text = "f = " + force.magnitude;	}
	}
}
