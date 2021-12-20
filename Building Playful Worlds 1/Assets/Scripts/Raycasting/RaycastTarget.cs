using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastTarget : MonoBehaviour
{

	public UnityEvent onRayHit;
	public delegate void OnHit();
	public event OnHit onRayHitEvent;

	public virtual void Hit(RaycastHit hit)
	{
		if (onRayHit != null)
			onRayHit.Invoke();

		onRayHitEvent?.Invoke();
	}

	public void Print(string message)
	{
		print(message);
	}

}
