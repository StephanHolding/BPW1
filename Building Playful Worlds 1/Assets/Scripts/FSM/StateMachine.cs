using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

	private Dictionary<System.Type, BaseState> states = new Dictionary<System.Type, BaseState>();
	private BaseState currentState;

	public void InitializeStateMachine(System.Type startState, params BaseState[] allStates)
	{
		for (int i = 0; i < allStates.Length; i++)
		{
			states.Add(allStates[i].GetType(), allStates[i]);
		}

		ChangeState(startState);
	}

	private void Update()
	{
		if (currentState != null)
		{
			currentState.OnStateUpdate();
		}
	}

	public void ChangeState(System.Type newState)
	{
		if (currentState != null)
			currentState.OnStateExited();

		currentState = states[newState];

		if (currentState != null)
			currentState.OnStateEntered();
	}

	public bool HasState(System.Type state)
	{
		return states.ContainsKey(state);
	}

}
