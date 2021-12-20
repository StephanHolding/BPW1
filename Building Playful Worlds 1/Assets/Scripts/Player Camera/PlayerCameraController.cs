using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

	public float mouseSensitivity;
	public int zoomStep;
	public Vector2 zoomMinMax;

	public bool clamp;
	[ShowByBool(true, nameof(clamp))]
	public Vector2 clampX, clampY;

	private Camera myCam;
	private float usedSensitivity;

	private void Awake()
	{
		myCam = GetComponent<Camera>();
		usedSensitivity = mouseSensitivity;
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		if (!GameManager.GameIsActive) return;

		MoveCamera();
		Zoom();
	}

	private void Zoom()
	{
		if (zoomStep == 0) return;

		float scroll = InputManager.InputActions.Player.Scroll.ReadValue<float>();

		if (scroll < 0)
		{
			if (myCam.fieldOfView + zoomStep <= zoomMinMax.y)
			{
				myCam.fieldOfView += zoomStep;
				usedSensitivity = CalculateSensitivityBasedOnZoom();
			}
		}
		else if (scroll > 0)
		{
			if (myCam.fieldOfView - zoomStep >= zoomMinMax.x)
			{
				myCam.fieldOfView -= zoomStep;
				usedSensitivity = CalculateSensitivityBasedOnZoom();
			}
		}
	}

	private void MoveCamera()
	{
		if (Cursor.visible) return;

		Vector2 inputVector = InputManager.InputActions.Player.Mouse.ReadValue<Vector2>();
		transform.Rotate(new Vector3(-inputVector.y, inputVector.x, 0) * usedSensitivity);

		if (clamp)
		{
			transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, clampX.x, clampX.y), Mathf.Clamp(transform.eulerAngles.y, clampY.x, clampY.y), 0);
		}
		else
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
		}

	}

	private float CalculateSensitivityBasedOnZoom()
	{
		return mouseSensitivity * (myCam.fieldOfView / zoomMinMax.y);
	}
}
