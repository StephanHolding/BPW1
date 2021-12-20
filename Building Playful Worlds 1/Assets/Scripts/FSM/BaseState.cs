using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseState
{

	protected StateMachine owner;

	public BaseState(StateMachine owner)
	{
		this.owner = owner;
	}

	public abstract void OnStateEntered();
	public abstract void OnStateUpdate();
	public abstract void OnStateExited();

}
