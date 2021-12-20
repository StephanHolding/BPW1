using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDetailToUI : MonoBehaviour
{

	[Header("Properties")]
	public bool displayTarget;

	[Header("UI References")]
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI genderText, ageText, placeText, jobText, skinText, hairText, pantsText, shirtText, shoesText, specialDetailText;

	private void Awake()
	{
		if (displayTarget)
			NPCGenerator.instance.onTargetDetermined += DisplayNPCDetailsOnUI;
	}

	private void OnDisable()
	{
		WipeDetails();
	}

	private void OnDestroy()
	{
		if (displayTarget)
			if (NPCGenerator.instance != null)
				NPCGenerator.instance.onTargetDetermined -= DisplayNPCDetailsOnUI;
	}

	public void DisplayNPCDetailsOnUI(NPCDetails NPCDetails)
	{
		nameText.text = NPCDetails.NPC_name;
		genderText.text = NPCDetails.NPC_gender == "Male" ? "M" : "F";
		ageText.text = NPCDetails.NPC_age.ToString();
		placeText.text = NPCDetails.NPC_place;
		jobText.text = NPCDetails.NPC_job;
		specialDetailText.text = NPCDetails.NPC_specialDetail;

		for (int i = 0; i < NPCDetails.NPC_appearances.Length; i++)
		{
			switch (NPCDetails.NPC_appearances[i].appearanceType)
			{
				case NPCDetails.NPCAppearance.AppearanceType.Skin:
					skinText.text = NPCDetails.NPC_appearances[i].appearanceColor.ToString();
					break;
				case NPCDetails.NPCAppearance.AppearanceType.Hair:
					hairText.text = NPCDetails.NPC_appearances[i].appearanceColor.ToString();
					break;
				case NPCDetails.NPCAppearance.AppearanceType.Shirt:
					shirtText.text = NPCDetails.NPC_appearances[i].appearanceColor.ToString();
					break;
				case NPCDetails.NPCAppearance.AppearanceType.Pants:
					pantsText.text = NPCDetails.NPC_appearances[i].appearanceColor.ToString();
					break;
				case NPCDetails.NPCAppearance.AppearanceType.Shoes:
					shoesText.text = NPCDetails.NPC_appearances[i].appearanceColor.ToString();
					break;
			}
		}
	}

	public void WipeDetails()
	{
		nameText.text = "-";
		genderText.text = "-";
		ageText.text = "-";
		placeText.text = "-";
		jobText.text = "-";
		specialDetailText.text = "-";
		skinText.text = "-";
		hairText.text = "-";
		shirtText.text = "-";
		pantsText.text = "-";
		shoesText.text = "-";
	}

}
