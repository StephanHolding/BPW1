using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Raycaster : MonoBehaviour
{

	public enum InputMode
	{
		CastOnButtonDown,
		CastOnButtonPressedContinuously,
		CastAlways
	}

	public enum TargetRequirement
	{
		None,
		CheckName,
		CheckTag,
		CheckLayer
	}

	[Header("Raycast Properties")]
	public float rayRange;
	public Transform rayOrigin;

	[Header("Target Requirements")]
	public TargetRequirement targetRequirement;
	[ShowByEnum(nameof(targetRequirement), 1)]
	public string objectName;
	[ShowByEnum(nameof(targetRequirement), 2)]
	public string objectTag;
	[ShowByEnum(nameof(targetRequirement), 3)]
	public int objectLayer;

	public InputMode inputMode;
	[ShowByEnum(nameof(inputMode), 0, 1)]
	public InputActionReference buttonForRaycast;

	protected virtual void Awake()
	{
		if (rayOrigin == null)
		{
			rayOrigin = transform;
		}

		if (inputMode == InputMode.CastOnButtonDown)
		{
			if (buttonForRaycast != null)
			{
				buttonForRaycast.action.Enable();
				buttonForRaycast.action.started += ButtonForRaycastStarted;
				buttonForRaycast.action.canceled += ButtonForRaycastCanceled;
			}
		}
	}

	protected virtual void OnDestroy()
	{
		if (inputMode == InputMode.CastOnButtonDown)
		{
			if (buttonForRaycast != null)
			{
				buttonForRaycast.action.started -= ButtonForRaycastStarted;
				buttonForRaycast.action.canceled -= ButtonForRaycastCanceled;
				buttonForRaycast.action.Disable();
			}
		}
	}

	private void ButtonForRaycastCanceled(InputAction.CallbackContext obj)
	{
		if (enabled)
			RayButtonUp();
	}

	private void ButtonForRaycastStarted(InputAction.CallbackContext obj)
	{
		if (enabled)
			RayButtonDown();
	}

	protected virtual void RayButtonDown()
	{
		RaycastTarget target = PerformRaycast<RaycastTarget>(out RaycastHit hit);
		if (target != null)
		{
			target.Hit(hit);
		}
	}

	protected virtual void RayButtonUp()
	{

	}

	protected virtual void RayButton()
	{
		RayButtonDown();
	}

	protected virtual void Update()
	{
		if (inputMode == InputMode.CastAlways)
		{
			RaycastTarget target = PerformRaycast<RaycastTarget>(out RaycastHit hit);
			if (target != null)
			{
				target.Hit(hit);
			}
		}

		if (inputMode == InputMode.CastOnButtonPressedContinuously)
		{
			if (buttonForRaycast.action.ReadValue<float>() == 1)
			{
				RayButton();
			}
		}
	}

	protected T PerformRaycast<T>(out RaycastHit rayHit)
	{
		Debug.DrawRay(rayOrigin.position, rayOrigin.forward * rayRange, Color.red);
		if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out RaycastHit hit, rayRange))
		{
			if (ConditionCheck(hit.transform.gameObject))
			{
				T target = hit.transform.GetComponent<T>();

				if (target != null)
				{
					rayHit = hit;
					return target;
				}
			}
		}

		rayHit = new RaycastHit();
		return default;
	}

	private bool ConditionCheck(GameObject target)
	{
		switch (targetRequirement)
		{
			case TargetRequirement.None:
				return true;
			case TargetRequirement.CheckName:
				return target.name == objectName;
			case TargetRequirement.CheckTag:
				return target.CompareTag(objectTag);
			case TargetRequirement.CheckLayer:
				return target.gameObject.layer == objectLayer;
			default:
				return false;
		}
	}
}
