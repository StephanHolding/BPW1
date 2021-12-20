using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sitting : AIState
{
	public Sitting(StateMachine owner) : base(owner)
	{
		this.owner = owner;
	}

	public override void OnStateEntered()
	{
		animator.SetInteger("AI State", 3);
	}

	public override void OnStateExited()
	{

	}

	public override void OnStateUpdate()
	{

	}
}
