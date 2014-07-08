using UnityEngine;
using XInputDotNetPure; // Required in C#

public class XinputHandler : MonoBehaviour
{
	public bool isDebugging = false;
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	
	public int indexNum = 1;
	
	// Use this for initialization
	void Start()
	{
		// No need to initialize anything for the plugin
	}
	
	// Update is called once per frame
	void Update()
	{
		// Find a PlayerIndex, for a single player game
		// Will find the controller in slot "indexNum" and use it
		if (!playerIndexSet || !prevState.IsConnected) 
		{
	
			PlayerIndex testPlayerIndex = (PlayerIndex)indexNum;
			GamePadState testState = GamePad.GetState(testPlayerIndex);
			if (testState.IsConnected)
			{
				//Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
				playerIndex = testPlayerIndex;
				playerIndexSet = true;
			}
			else
			{
				Debug.LogWarning("Controller Index:" + playerIndex + " is Not Connected");
			}
		}

		
		prevState = state;
		state = GamePad.GetState(playerIndex);
/*		
		// Detect if a button was pressed this frame
		if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
		{
			renderer.material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
		}
		// Detect if a button was released this frame
		if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
		{
			renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
		
		// Set vibration according to triggers
		GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);
		
		// Make the current object turn
		transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);
*/	}



	public Vector2 GetLeftStick()
	{
		return new Vector2(state.ThumbSticks.Left.X , state.ThumbSticks.Left.Y);
	}
	public Vector2 GetLastLeftStick()
	{
		return new Vector2(prevState.ThumbSticks.Left.X, prevState.ThumbSticks.Left.Y);
	}



	public Vector2 GetRightStick()
	{
		return new Vector2(state.ThumbSticks.Right.X , state.ThumbSticks.Right.Y);
	}
	public Vector2 GetLastRightStick()
	{
		return new Vector2(prevState.ThumbSticks.Right.X, prevState.ThumbSticks.Right.Y);
	}



	public float GetLeftTrigger()
	{
		return state.Triggers.Left;
	}
	public float GetLastLeftTrigger()
	{
		return prevState.Triggers.Left;
	}



	public float GetRightTrigger()
	{
		return state.Triggers.Right;
	}
	public float GetLastRightTrigger()
	{
		return prevState.Triggers.Right;
	}



	public void SetVibration(Vector2 par1Vec)
	{
		GamePad.SetVibration(playerIndex, par1Vec.x, par1Vec.y);
	}



	public bool GetButtonDown(string parButtName)
	{
		return (  (GetLastButton(parButtName) == false)  &&  (GetButton(parButtName) == true)  );
	}

	public bool GetButtonUp(string parButtName)
	{
		return (  (GetLastButton(parButtName) == true)  &&  (GetButton(parButtName) == false)  );
	}


	public bool GetButton(string parButName)
	{
		if(parButName == "A")
		{
			return state.Buttons.A == ButtonState.Pressed;
		}
		else if(parButName == "B")
		{
			return state.Buttons.B == ButtonState.Pressed;
		}
		else if(parButName == "X")
		{
			return state.Buttons.X == ButtonState.Pressed;
		}
		else if(parButName == "Y")
		{
			return state.Buttons.Y == ButtonState.Pressed;
		}
		else if(parButName == "LB")
		{
			return state.Buttons.LeftShoulder == ButtonState.Pressed;
		}
		else if(parButName == "RB")
		{
			return state.Buttons.RightShoulder == ButtonState.Pressed;
		}
		else if(parButName == "RightStick")
		{
			return state.Buttons.RightStick == ButtonState.Pressed;
		}
		else if(parButName == "LeftStick")
		{
			return state.Buttons.LeftStick == ButtonState.Pressed;
		}
		else if(parButName == "Start")
		{
			return state.Buttons.Start == ButtonState.Pressed;
		}
		else if(parButName == "Back")
		{
			return state.Buttons.Back == ButtonState.Pressed;
		}

		return false;
	}


	public bool GetLastButton(string parButName)
	{
		if(parButName == "A")
		{
			return prevState.Buttons.A == ButtonState.Pressed;
		}
		else if(parButName == "B")
		{
			return prevState.Buttons.B == ButtonState.Pressed;
		}
		else if(parButName == "X")
		{
			return prevState.Buttons.X == ButtonState.Pressed;
		}
		else if(parButName == "Y")
		{
			return prevState.Buttons.Y == ButtonState.Pressed;
		}
		else if(parButName == "LB")
		{
			return prevState.Buttons.LeftShoulder == ButtonState.Pressed;
		}
		else if(parButName == "RB")
		{
			return prevState.Buttons.RightShoulder == ButtonState.Pressed;
		}
		else if(parButName == "RightStick")
		{
			return prevState.Buttons.RightStick == ButtonState.Pressed;
		}
		else if(parButName == "LeftStick")
		{
			return prevState.Buttons.LeftStick == ButtonState.Pressed;
		}
		else if(parButName == "Start")
		{
			return prevState.Buttons.Start == ButtonState.Pressed;
		}
		else if(parButName == "Back")
		{
			return prevState.Buttons.Back == ButtonState.Pressed;
		}
		
		return false;
	}


	void OnGUI()
	{
		if(isDebugging)
		{
			string text = "Use left stick to turn the cube, hold A to change color\n";
			text += string.Format("This be controller Input Number: " + indexNum + " \n");
			text += string.Format("IsConnected {0} Packet #{1}\n", state.IsConnected, state.PacketNumber);
			text += string.Format("\tTriggers {0} {1}\n", state.Triggers.Left, state.Triggers.Right);
			text += string.Format("\tD-Pad {0} {1} {2} {3}\n", state.DPad.Up, state.DPad.Right, state.DPad.Down, state.DPad.Left);
			text += string.Format("\tButtons Start {0} Back {1}\n", state.Buttons.Start, state.Buttons.Back);
			text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", state.Buttons.LeftStick, state.Buttons.RightStick, state.Buttons.LeftShoulder, state.Buttons.RightShoulder);
			text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", state.Buttons.A, state.Buttons.B, state.Buttons.X, state.Buttons.Y);
			text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
		}
	}
}
