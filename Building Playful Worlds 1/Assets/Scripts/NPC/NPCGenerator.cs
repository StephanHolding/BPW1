using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : SingletonTemplateMono<NPCGenerator>
{

	public NPCDetailsDatabase npcDatabase;
	public bool spawnNpcsDynamically = true;
	public bool makeExistingNpcTarget;
	public Transform[] spawnTransformsDyncamicNPC;
	public int spawnTargetAfterTimeInSeconds;

	public NPCDetails TargetDetails { get; private set; }

	public delegate void NPCEvent(NPCDetails npcDetails);
	public event NPCEvent onTargetDetermined;

	private bool targetHasBeenSpawned;

	private void Start()
	{
		DetermineTarget();
		InitializeExistingNPCs();

		if (spawnNpcsDynamically)
		{
			StartCoroutine(NPCSpawnLoop());
			StartCoroutine(TargetSpawnCoroutine());
		}
	}

	private IEnumerator NPCSpawnLoop()
	{
		while (GameManager.GameIsActive)
		{
			yield return new WaitForSeconds(Random.Range(10, 30));
			SpawnNPC(GetRandomSpawnTransform());
		}
	}

	private IEnumerator TargetSpawnCoroutine()
	{
		yield return new WaitForSeconds(spawnTargetAfterTimeInSeconds);

		while (GameManager.GameIsActive && !targetHasBeenSpawned)
		{
			int chance = Random.Range(0, 4);
			if (chance == 0)
			{
				SpawnNPC(GetRandomSpawnTransform(), true);
			}

			yield return new WaitForSeconds(2);
		}
	}

	private void InitializeExistingNPCs()
	{
		NPC[] existingNPCs = FindObjectsOfType<NPC>();
		int targetIndex = -1;

		if (makeExistingNpcTarget)
		{
			targetIndex = Random.Range(0, existingNPCs.Length);
		}

		for (int i = 0; i < existingNPCs.Length; i++)
		{
			if (makeExistingNpcTarget && i == targetIndex)
			{
				FillNPCDetails(existingNPCs[i], true);
				existingNPCs[i].StartNPC(GetRandomSpawnTransform());
			}
			else
			{
				FillNPCDetails(existingNPCs[i], false);
				existingNPCs[i].StartNPC(GetRandomSpawnTransform());
			}
		}
	}

	private NPC SpawnNPC(Transform spawnTransform, bool target = false)
	{
		print("Spawning NPC! Is target: " + target);

		GameObject prefabToSpawn = Resources.Load<GameObject>("NPC Prefab");
		GameObject newObject = Instantiate(prefabToSpawn, spawnTransform.position, spawnTransform.rotation);
		NPC toReturn = newObject.GetComponent<NPC>();
		toReturn = FillNPCDetails(toReturn, target);

		toReturn.StartNPC(GetFurthestSpawnTransform(spawnTransform.position));

		if (target)
			targetHasBeenSpawned = true;

		return toReturn;
	}

	private NPC FillNPCDetails(NPC npc, bool target = false)
	{
		NPCDetails details = null;

		if (target)
		{
			details = TargetDetails;
		}
		else
		{
			bool createdUniqueNPC = false;
			while (!createdUniqueNPC)
			{
				details = GenerateNPCDetails();

				if (targetHasBeenSpawned)
					createdUniqueNPC = !NpcDetailsIsSameAsTarget(details);
				else
					createdUniqueNPC = true;
			}
		}

		npc.details = details;
		npc.isTarget = target;
		return npc;
	}

	public bool NpcDetailsIsSameAsTarget(NPCDetails npc)
	{
		return TargetDetails.Compare(npc);
	}

	private void DetermineTarget()
	{
		TargetDetails = GenerateNPCDetails();
		onTargetDetermined?.Invoke(TargetDetails);
	}

	private Transform GetRandomSpawnTransform()
	{
		return spawnTransformsDyncamicNPC[Random.Range(0, spawnTransformsDyncamicNPC.Length)];
	}

	private Transform GetFurthestSpawnTransform(Vector3 currentPosition)
	{
		Transform toReturn = spawnTransformsDyncamicNPC[0];
		float cachedDistance = Vector3.Distance(currentPosition, toReturn.position);

		for (int i = 1; i < spawnTransformsDyncamicNPC.Length; i++)
		{
			float distance = Vector3.Distance(currentPosition, spawnTransformsDyncamicNPC[i].position);
			if (distance > cachedDistance)
			{
				toReturn = spawnTransformsDyncamicNPC[i];
				cachedDistance = distance;
			}
		}

		return toReturn;
	}

	private NPCDetails GenerateNPCDetails()
	{
		NPCDetails toReturn = new NPCDetails()
		{
			NPC_gender = (string)npcDatabase.GetRandomDetail(NPCDetails.DetailType.Gender),
			NPC_age = (int)npcDatabase.GetRandomDetail(NPCDetails.DetailType.Age),
			NPC_place = (string)npcDatabase.GetRandomDetail(NPCDetails.DetailType.PlaceOfResidence),
			NPC_job = (string)npcDatabase.GetRandomDetail(NPCDetails.DetailType.Job),
			NPC_appearances = (NPCDetails.NPCAppearance[])npcDatabase.GetRandomDetail(NPCDetails.DetailType.Appearance),
			NPC_specialDetail = (string)npcDatabase.GetRandomDetail(NPCDetails.DetailType.SpecialDetail),
		};

		toReturn.NPC_name = (string)npcDatabase.GetGenderedName(toReturn.NPC_gender == "Male");

		return toReturn;
	}

}
