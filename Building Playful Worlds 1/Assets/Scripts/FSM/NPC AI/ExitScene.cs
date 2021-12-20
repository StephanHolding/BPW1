using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScene : AIState
{
	public ExitScene(StateMachine owner) : base(owner)
	{
		this.owner = owner;
	}

	public override void OnStateEntered()
	{
		if (SetAgentDestination(npc.DesiredExit.position))
		{
			OnReachedDestination += ReachedDestination;
		}

		animator.SetInteger("AI State", 1);
	}

	public override void OnStateExited()
	{

	}

	public override void OnStateUpdate()
	{
		CheckAgentReachedDestination();
	}

	private void ReachedDestination()
	{
		npc.StopNPC();
	}
}
