using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDetailsScanner : Raycaster
{

	private NPCDetailToUI npcToUI;
	private CameraState camState;

	protected override void Awake()
	{
		base.Awake();
		npcToUI = GetComponentInChildren<NPCDetailToUI>();
		camState = GetComponent<CameraState>();
		camState.OnStateActivated += ScannerSoundEffect;
	}
	protected override void OnDestroy()
	{
		base.OnDestroy();
		camState.OnStateActivated -= ScannerSoundEffect;
	}

	protected override void RayButtonDown()
	{
		BulletTarget bulletTarget = PerformRaycast<BulletTarget>(out RaycastHit hit);
		if (bulletTarget != null)
		{
			NPC npc = bulletTarget.citizen;
			if (npc != null)
			{
				EffectsManager.instance.PlayAudio("Scan");
				ShowDetailsOnScreen(npc);
			}
		}
	}

	private void ShowDetailsOnScreen(NPC npc)
	{
		npcToUI.DisplayNPCDetailsOnUI(npc.details);
	}

	private void ScannerSoundEffect()
	{
		EffectsManager.instance.PlayAudio("Night Vision Goggles");
	}

}
