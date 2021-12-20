using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoStuff : AIState
{
	public DoStuff(StateMachine owner) : base(owner)
	{
		this.owner = owner;
	}

	public override void OnStateEntered()
	{
		Debug.Log("Doing stuff!");
		animator.SetInteger("AI State", 2);
		animator.SetInteger("Randomizer", Random.Range(1, 1));

		if (owner.HasState(typeof(ExitScene)))
		{
			OnAnimationEnded += StuffHasBeenDone;
		}

	}

	public override void OnStateExited()
	{
		if (owner.HasState(typeof(ExitScene)))
		{
			OnAnimationEnded -= StuffHasBeenDone;
			animator.SetInteger("Randomizer", 0);
		}
	}

	public override void OnStateUpdate()
	{
		CheckAnimationEnded("Do Stuff");
	}


	private void StuffHasBeenDone()
	{
		if (owner.HasState(typeof(ExitScene)))
			owner.ChangeState(typeof(ExitScene));
	}
}
