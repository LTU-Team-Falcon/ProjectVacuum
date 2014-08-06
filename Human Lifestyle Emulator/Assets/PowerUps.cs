using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour {


	public PhysicsFPSWalker PhysicsFPSWalker;

	//Duh...
	private int IronSkinCounter;
	private bool IronSkinPowerUpActive;


	//Duh...
	private int GoldenBootsCounter;
	private bool GoldenBootsPowerUpActive;


	private int SuperJumpCounter;
	private bool SuperJumpPowerUpActive;

	// Use this for initialization
	void Start () {

		PhysicsFPSWalker = gameObject.GetComponent<PhysicsFPSWalker>();

	}
	
	// Update is called once per frame
	void Update () {

		if (IronSkinPowerUpActive == true)
		{
			IronSkinCounter++;
		}

		if (GoldenBootsPowerUpActive == true)
		{
			GoldenBootsCounter++;
		}

		if (SuperJumpPowerUpActive == true)
		{
			SuperJumpCounter++;
		}

		if (IronSkinCounter > 450) 
		{
			IronSkinPowerUpActive = false;
			PhysicsFPSWalker.force = PhysicsFPSWalker.force / 10;
			rigidbody.mass = rigidbody.mass / 4;
			rigidbody.drag = rigidbody.drag / 4;
			IronSkinCounter = 0;
		}
	
		if (GoldenBootsCounter > 400) 
		{
			GoldenBootsPowerUpActive = false;
			PhysicsFPSWalker.TimeTillDash = 77;
			GoldenBootsCounter = 0;
		}

		if (SuperJumpCounter > 400)
		{
			SuperJumpPowerUpActive = false;
			SuperJumpCounter = 0;
		}

	}

	void OnTriggerEnter (Collider col)
	{
		//Make Heavy Power Up
		if (col.gameObject.name == "Iron Skin Power Up") 
		{
			if (IronSkinPowerUpActive == true)
			{
				//if its already active
				IronSkinPowerUpActive = false;
				PhysicsFPSWalker.force = PhysicsFPSWalker.force / 10;
				rigidbody.mass = rigidbody.mass / 4;
				rigidbody.drag = rigidbody.drag / 4;
				IronSkinCounter = 0;
			}

			IronSkinPowerUpActive = true;
			//PhysicsFPSWalker = gameObject.GetComponent<PhysicsFPSWalker>();
			PhysicsFPSWalker.force = PhysicsFPSWalker.force * 10;
			rigidbody.mass = rigidbody.mass * 4;
			rigidbody.drag = rigidbody.drag * 4;
		}

		//Multi Jump Power Up
		if (col.gameObject.name == "Golden Legs Power Up") 
		{
			if (GoldenBootsPowerUpActive == true)
			{
				//if its already active
				GoldenBootsPowerUpActive = false;
				PhysicsFPSWalker.TimeTillDash = 77;
				GoldenBootsCounter = 0;
			}

			Debug.Log("HIT");
			GoldenBootsPowerUpActive = true;
			PhysicsFPSWalker.TimeTillDash = 30;

		}

		if (col.gameObject.name == "Super Jump Power Up")
		{
			if (SuperJumpPowerUpActive == true)
			{
				//if its already active
				SuperJumpPowerUpActive = false;
				PhysicsFPSWalker.JumpCount = 0;
				PhysicsFPSWalker.SuperJumpActive = false;
				SuperJumpCounter = 0;
			}
			SuperJumpPowerUpActive = true;
			PhysicsFPSWalker.SuperJumpActive = true;
		}
	}
}
