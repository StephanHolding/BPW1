using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : SingletonTemplateMono<GameManager>
{
	public enum GameOverType
	{
		None,
		ShotCorrectNPC,
		ShotIncorrectNPC,
		TargetGotAway
	}


	public static bool GameIsActive { get; private set; }

	public TextMeshProUGUI gameOverText, pressAnyKeyText;

	private Canvas gameOverCanvas;
	private GameOverType currentGameOverType;

	protected override void Awake()
	{
		GameIsActive = true;
		base.Awake();
		gameOverCanvas = GetComponentInChildren<Canvas>(true);
	}

	private void Start()
	{
		InputManager.InputActions.Player.AnyKey.started += AnyKeyPressed;
	}

	private void OnDestroy()
	{
		InputManager.InputActions.Player.AnyKey.started -= AnyKeyPressed;
	}

	private void AnyKeyPressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		if (!GameIsActive)
			GameOverContinueAction();
	}

	public void GameOver(GameOverType gameOverType)
	{
		currentGameOverType = gameOverType;
		SetGameOverTexts(gameOverType);
		StartCoroutine(SlowGameTime());
	}

	public void GameOver(bool correctNPC)
	{
		GameOver(CorrectNPCBoolToGameOverType(correctNPC));
	}

	public GameOverType CorrectNPCBoolToGameOverType(bool correctNPC)
	{
		return correctNPC ? GameOverType.ShotCorrectNPC : GameOverType.ShotIncorrectNPC;
	}

	private IEnumerator SlowGameTime()
	{
		while (Time.timeScale > 0.1f)
		{
			Time.timeScale -= Time.deltaTime / 1.5f;
			yield return new WaitForEndOfFrame();
		}

		Time.timeScale = 0;
		GameIsActive = false;
		gameOverCanvas.gameObject.SetActive(true);
	}

	private void SetGameOverTexts(GameOverType gameOverType)
	{
		switch (gameOverType)
		{
			case GameOverType.ShotCorrectNPC:
				gameOverText.text = "Good work.";
				pressAnyKeyText.text = "Press any key to continue";
				break;
			case GameOverType.ShotIncorrectNPC:
				gameOverText.text = "You shot the wrong person!";
				pressAnyKeyText.text = "Press any key to retry";
				break;
			case GameOverType.TargetGotAway:
				gameOverText.text = "The target got away...";
				pressAnyKeyText.text = "Press any key to retry";
				break;
		}
	}

	private void GameOverContinueAction()
	{
		GameIsActive = true;
		Time.timeScale = 1;
		SceneHandler sceneHandler = SceneHandler.instance;

		if (currentGameOverType == GameOverType.ShotCorrectNPC)
		{
			if (sceneHandler.NextSceneIsPresent())
				sceneHandler.LoadNextScene();
			else
				sceneHandler.LoadScene(0);
		}
		else if (currentGameOverType == GameOverType.ShotIncorrectNPC || currentGameOverType == GameOverType.TargetGotAway)
		{
			sceneHandler.ReloadCurrentScene();
		}
	}

}
