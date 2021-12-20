using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{

	public enum NPCMode
	{
		Walking,
		Sitting,
		Standing
	}

	public NPCMode npcMode;
	public NPCDetails details;
	public Transform maleAppearance, femaleAppearance;
	public Transform DesiredExit { get; private set; }
	public bool isTarget;

	protected Rigidbody[] allRigidBodies;
	private StateMachine FSM;
	private Animator animator;
	private NavMeshAgent agent;

	public virtual void StartNPC(Transform desiredTarget)
	{
		FSM = GetComponent<StateMachine>();

		this.DesiredExit = desiredTarget;
		ApplyAppearance();
		allRigidBodies = GetComponentsInChildren<Rigidbody>();
		animator = GetComponentInChildren<Animator>();
		agent = GetComponent<NavMeshAgent>();
		ToggleRagdoll(false);

		if (npcMode == NPCMode.Walking)
		{
			FSM.InitializeStateMachine(typeof(ExitScene), new WalkTowardsTarget(FSM), new DoStuff(FSM), new ExitScene(FSM), new Dead(FSM));
		}
		else if (npcMode == NPCMode.Sitting)
		{
			FSM.InitializeStateMachine(typeof(Sitting), new Sitting(FSM), new Dead(FSM));
			agent.enabled = false;
		}
		else if (npcMode == NPCMode.Standing)
		{
			FSM.InitializeStateMachine(typeof(DoStuff), new DoStuff(FSM), new Dead(FSM));
			agent.enabled = false;
		}

	}

	public virtual void StopNPC()
	{
		print("Destroying NPC! Target: " + isTarget);

		if (isTarget)
		{
			GameManager.instance.GameOver(GameManager.GameOverType.TargetGotAway);
		}

		Destroy(gameObject);
	}

	public void ApplyAppearance()
	{
		if (details.NPC_gender == "Male")
		{
			maleAppearance.gameObject.SetActive(true);
		}
		else
		{
			femaleAppearance.gameObject.SetActive(true);
		}

		for (int i = 0; i < details.NPC_appearances.Length; i++)
		{
			NPCDetails.NPCAppearance appearance = details.NPC_appearances[i];

			switch (appearance.appearanceType)
			{
				case NPCDetails.NPCAppearance.AppearanceType.Skin:
					SetSkinColor(appearance.appearanceColor);
					break;
				case NPCDetails.NPCAppearance.AppearanceType.Shirt:
					SetClothingColor("Shirt", appearance.appearanceColor);
					break;
				case NPCDetails.NPCAppearance.AppearanceType.Hair:
					SetClothingColor("Hair", appearance.appearanceColor);
					break;
				case NPCDetails.NPCAppearance.AppearanceType.Pants:
					SetClothingColor("Pants", appearance.appearanceColor);
					break;
				case NPCDetails.NPCAppearance.AppearanceType.Shoes:
					SetClothingColor("Shoes", appearance.appearanceColor);
					break;
			}
		}
	}

	private void SetSkinColor(NamedColor.NamedColorEnum namedColor)
	{
		SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

		for (int i = 0; i < skinnedMeshRenderers.Length; i++)
		{
			foreach (Material m in skinnedMeshRenderers[i].materials)
			{
				if (m.name.Contains("Skin"))
				{
					SetMaterialColor(m, NamedColor.NamedColorToSkinColor(namedColor));
				}
			}
		}
	}

	private void SetClothingColor(string materialName, NamedColor.NamedColorEnum namedColor)
	{
		SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

		for (int i = 0; i < skinnedMeshRenderers.Length; i++)
		{
			foreach (Material m in skinnedMeshRenderers[i].materials)
			{
				if (m.name.Contains(materialName))
				{
					SetMaterialColor(m, NamedColor.NamedColorToRGB(namedColor));
				}
			}
		}
	}

	private void SetMaterialColor(Material material, Color color)
	{
		material.SetColor("_BaseColor", color);
	}

	public void Die()
	{
		FSM.ChangeState(typeof(Dead));
		ToggleRagdoll(true);
		bool killedCorrectNPC = NPCGenerator.instance.NpcDetailsIsSameAsTarget(details);
		GameManager.instance.GameOver(killedCorrectNPC);
	}

	public void ToggleRagdoll(bool toggle)
	{
		for (int i = 0; i < allRigidBodies.Length; i++)
		{
			allRigidBodies[i].isKinematic = !toggle;
		}

		agent.enabled = !toggle;
		animator.enabled = !toggle;
	}
}
