using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraState : MonoBehaviour
{
	public Transform meshToDisable;
	public bool isStartState;

	public static CameraState currentState;
	[HideInInspector]
	public CameraState previousState;

	protected bool active;
	protected Camera myCam;
	protected PlayerCameraController cameraController;
	protected Volume volume;
	protected Transform overlay;
	protected Raycaster raycaster;

	public delegate void CameraStateEvent();
	public event CameraStateEvent OnStateActivated;

	protected virtual void Awake()
	{
		myCam = GetComponent<Camera>();
		cameraController = GetComponent<PlayerCameraController>();
		volume = GetComponent<Volume>();
		raycaster = GetComponent<Raycaster>();

		if (transform.childCount > 0)
			overlay = transform.GetChild(0);
	}

	private void Start()
	{
		if (isStartState)
		{
			if (currentState == null)
				EnableState();
			else
				Debug.LogError("Current state was already filled. Is there more than 1 StartState?");
		}
		else
		{
			DisableState();
		}
	}

	private void Update()
	{
		if (!active) return;

		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			print("pressed");
			ReturnToPreviousState();
		}
	}

	protected void ReturnToPreviousState()
	{
		if (previousState != null)
		{
			previousState.EnableState();
		}
	}

	public virtual void EnableState()
	{
		StartCoroutine(SwitchCoroutine());
	}

	public virtual void DisableState()
	{
		print("Disabling");

		myCam.enabled = false;

		if (cameraController != null)
			cameraController.enabled = false;

		if (volume != null)
			volume.enabled = false;

		if (raycaster != null)
			raycaster.enabled = false;

		overlay?.gameObject.SetActive(false);
		active = false;

		if (meshToDisable != null)
			meshToDisable.gameObject.SetActive(true);
	}

	private IEnumerator SwitchCoroutine()
	{
		print("entering " + gameObject.name);

		if (currentState != null && currentState != this)
		{
			previousState = currentState;
			previousState.DisableState();
		}

		currentState = this;

		yield return new WaitForEndOfFrame();

		myCam.enabled = true;

		if (cameraController != null)
			cameraController.enabled = true;

		if (volume != null)
			volume.enabled = true;

		overlay?.gameObject.SetActive(true);
		active = true;

		if (raycaster != null)
			raycaster.enabled = true;

		if (meshToDisable != null)
		{
			meshToDisable.gameObject.SetActive(false);
		}

		OnStateActivated?.Invoke();
	}

}
