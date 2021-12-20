using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIState : BaseState
{

	protected NavMeshAgent agent;
	protected Animator animator;
	protected NPC npc;
	protected delegate void AIEvent();

	protected event AIEvent OnReachedDestination;
	protected event AIEvent OnAnimationEnded;

	public AIState(StateMachine owner) : base(owner)
	{
		this.owner = owner;
		agent = owner.GetComponent<NavMeshAgent>();
		animator = owner.GetComponentInChildren<Animator>();
		npc = owner.GetComponent<NPC>();
	}

	protected bool SetAgentDestination(Vector3 position)
	{
		if (NavMesh.SamplePosition(position, out NavMeshHit hit, 2, -1))
		{
			agent.SetDestination(hit.position);
			return true;
		}
		else
		{
			Debug.LogError(position + " could not be reached by AI");
			return false;
		}
	}

	protected void CheckAgentReachedDestination()
	{
		if (agent.hasPath && agent.remainingDistance <= agent.stoppingDistance)
		{
			if (OnReachedDestination != null)
			{
				OnReachedDestination.Invoke();
			}
		}
	}

	protected void CheckAnimationEnded(string tag)
	{
		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		if (stateInfo.IsTag(tag))
		{
			if (stateInfo.normalizedTime >= 0.99f)
			{
				if (OnAnimationEnded != null)
				{
					OnAnimationEnded.Invoke();
				}
			}
		}

	}
}
