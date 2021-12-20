using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkTowardsTarget : AIState
{

	private Transform target;

	public WalkTowardsTarget(StateMachine owner) : base(owner)
	{
		this.owner = owner;
	}

	public override void OnStateEntered()
	{
		target = NPCTargetManager.instance.GetRandomInteractionTarget();

		if (SetAgentDestination(target.position))
		{
			OnReachedDestination += ReachedDestination;
		}

		animator.SetInteger("AI State", 1);
	}

	public override void OnStateUpdate()
	{
		CheckAgentReachedDestination();
	}

	public override void OnStateExited()
	{
		OnReachedDestination -= ReachedDestination;
	}

	private void ReachedDestination()
	{
		owner.ChangeState(typeof(DoStuff));
	}
}
