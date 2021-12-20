using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : AIState
{
	public Dead(StateMachine owner) : base(owner)
	{
		this.owner = owner;
	}

	public override void OnStateEntered()
	{
		//throw new System.NotImplementedException();
	}

	public override void OnStateExited()
	{
		//throw new System.NotImplementedException();
	}

	public override void OnStateUpdate()
	{
		//throw new System.NotImplementedException();
	}
}
