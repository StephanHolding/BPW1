using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTargetManager : SingletonTemplateMono<NPCTargetManager>
{

	public Transform exit;
	public NPCInteractionPoint[] interactionTargets;

	private void Start()
	{
		interactionTargets = GetComponentsInChildren<NPCInteractionPoint>();
	}

	public Transform GetRandomInteractionTarget()
	{
		return interactionTargets[Random.Range(0, interactionTargets.Length)].pointOfInteraction;
	}

}
