using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public void StartGame()
	{
		print("Pressed");
		SceneHandler.instance.LoadNextScene();
	}

	public void Exit()
	{
		Application.Quit();
	}
}
