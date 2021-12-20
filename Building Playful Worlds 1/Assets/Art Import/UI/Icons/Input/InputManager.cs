using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{

	public static InputActions InputActions { get; private set; }

	static InputManager()
	{
		if (InputActions == null)
		{
			InputActions = new InputActions();
			InputActions.Enable();
		}
	}

}
