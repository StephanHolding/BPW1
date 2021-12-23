using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : SingletonTemplateMono<CursorManager>
{

	public enum CursorLockOnStart
	{
		NoChange,
		Lock,
		Unlock
	}

	public enum CursorVisiblityOnStart
	{
		NoChange,
		Show,
		Hide
	}

	public static void LockAndHide()
	{
		ToggleCursorLock(true);
		ToggleCursorVisiblity(false);
	}

	public static void UnlockAndShow()
	{
		ToggleCursorLock(false);
		ToggleCursorVisiblity(true);
	}

	public static void ToggleCursorLock(bool toggle)
	{
		Cursor.lockState = toggle ? CursorLockMode.Locked : CursorLockMode.None;
	}

	public static void ToggleCursorVisiblity(bool toggle)
	{
		Cursor.visible = toggle;
	}

	public CursorLockOnStart cursorLockOnStart;
	public CursorVisiblityOnStart cursorVisiblityOnStart;

	private static bool desiredVisibility;
	private static CursorLockMode desiredLockState;

	private void Start()
	{
		ExecuteLockAndVisiblityOnStart();
	}

	private void OnApplicationFocus(bool focus)
	{
		if (focus)
		{
			ReapplyDesiredStates();
		}
	}

	private void ExecuteLockAndVisiblityOnStart()
	{
		switch (cursorLockOnStart)
		{
			case CursorLockOnStart.Lock:
				ToggleCursorLock(true);
				break;
			case CursorLockOnStart.Unlock:
				ToggleCursorLock(false);
				break;
		}

		switch (cursorVisiblityOnStart)
		{
			case CursorVisiblityOnStart.Show:
				ToggleCursorVisiblity(true);
				break;
			case CursorVisiblityOnStart.Hide:
				ToggleCursorVisiblity(false);
				break;
		}

		desiredLockState = Cursor.lockState;
		desiredVisibility = Cursor.visible;
	}

	private void ReapplyDesiredStates()
	{
		Cursor.visible = desiredVisibility;
		Cursor.lockState = desiredLockState;
	}
}
