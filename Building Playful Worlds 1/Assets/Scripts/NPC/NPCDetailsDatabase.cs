using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NPCDetailsDatabase : ScriptableObject
{
	public string[] firstNamesMale;
	public string[] firstNamesFemale;
	public string[] lastNames;
	public int minAge, maxAge;
	public string[] genders;
	public string[] allPlacesOfResidence;
	public string[] allJobs;
	public NPCDetails.NPCAppearance[] skinColors;
	public NPCDetails.NPCAppearance[] hairColors;
	public NPCDetails.NPCAppearance[] shirtColors;
	public NPCDetails.NPCAppearance[] pantsColors;
	public NPCDetails.NPCAppearance[] shoeColors;
	public string[] specialDetails;

	public object GetRandomDetail(NPCDetails.DetailType type)
	{
		switch (type)
		{
			case NPCDetails.DetailType.Age:
				return Random.Range(minAge, maxAge);
			case NPCDetails.DetailType.Gender:
				return genders[Random.Range(0, genders.Length)];
			case NPCDetails.DetailType.PlaceOfResidence:
				return allPlacesOfResidence[Random.Range(0, allPlacesOfResidence.Length)];
			case NPCDetails.DetailType.Job:
				return allJobs[Random.Range(0, allJobs.Length)];
			case NPCDetails.DetailType.Appearance:
				return GetRandomAppearanceFromEachType();
			case NPCDetails.DetailType.SpecialDetail:
				return specialDetails[Random.Range(0, specialDetails.Length)];
			default:
				return null;
		}
	}

	public object GetGenderedName(bool male)
	{
		string toReturn = string.Empty;

		if (male)
			toReturn = firstNamesMale[Random.Range(0, firstNamesMale.Length)] + " " + lastNames[Random.Range(0, lastNames.Length)];
		else
			toReturn = firstNamesFemale[Random.Range(0, firstNamesFemale.Length)] + " " + lastNames[Random.Range(0, lastNames.Length)];

		return toReturn;
	}

	[ContextMenu("Fill All Fields")]
	private void FillFieldsFromFile()
	{
		firstNamesMale = TextFileToStringArray("FirstNamesMale");
		firstNamesFemale = TextFileToStringArray("FirstNamesFemale");
		lastNames = TextFileToStringArray("LastNames");
		minAge = 20;
		maxAge = 80;
		genders = new string[] { "Male", "Female" };
		allPlacesOfResidence = TextFileToStringArray("PlacesOfResidence");
		allJobs = TextFileToStringArray("Jobs");
		skinColors = GenerateAppearanceOfSkinColors(NPCDetails.NPCAppearance.AppearanceType.Skin);
		hairColors = GenerateAppearanceOfSkinColors(NPCDetails.NPCAppearance.AppearanceType.Hair);
		shirtColors = GenerateAppearanceOfAllColors(NPCDetails.NPCAppearance.AppearanceType.Shirt);
		pantsColors = GenerateAppearanceOfAllColors(NPCDetails.NPCAppearance.AppearanceType.Pants);
		shoeColors = GenerateAppearanceOfAllColors(NPCDetails.NPCAppearance.AppearanceType.Shoes);
		specialDetails = TextFileToStringArray("SpecialDetails");
	}

	private string[] TextFileToStringArray(string fileName)
	{
		TextAsset textFile = Resources.Load<TextAsset>("Text Files/" + fileName);
		string allText = textFile.text;
		allText = allText.Replace("\n", "");
		allText = allText.Replace("\r", "");
		return allText.Split(';');
	}

	private NPCDetails.NPCAppearance[] GenerateAppearanceOfAllColors(NPCDetails.NPCAppearance.AppearanceType type)
	{
		List<NPCDetails.NPCAppearance> toReturn = new List<NPCDetails.NPCAppearance>();

		for (int i = 0; i < System.Enum.GetValues(typeof(NamedColor.NamedColorEnum)).Length; i++)
		{
			toReturn.Add(new NPCDetails.NPCAppearance(type, (NamedColor.NamedColorEnum)i));
		}

		return toReturn.ToArray();
	}

	private NPCDetails.NPCAppearance[] GenerateAppearanceOfSkinColors(NPCDetails.NPCAppearance.AppearanceType type)
	{
		List<NPCDetails.NPCAppearance> toReturn = new List<NPCDetails.NPCAppearance>();

		toReturn.Add(new NPCDetails.NPCAppearance(type, NamedColor.NamedColorEnum.Black));
		toReturn.Add(new NPCDetails.NPCAppearance(type, NamedColor.NamedColorEnum.White));
		toReturn.Add(new NPCDetails.NPCAppearance(type, NamedColor.NamedColorEnum.Brown));

		return toReturn.ToArray();
	}

	private  NPCDetails.NPCAppearance[] GetRandomAppearanceFromEachType()
	{
		int appearanceTypeCount = System.Enum.GetValues(typeof(NPCDetails.NPCAppearance.AppearanceType)).Length;
		NPCDetails.NPCAppearance[] toReturn = new NPCDetails.NPCAppearance[appearanceTypeCount];

		for (int i = 0; i < appearanceTypeCount; i++)
		{
			toReturn[i] = GetRandomAppearanceOfType(i);
		}

		return toReturn;
	}

	private NPCDetails.NPCAppearance GetRandomAppearanceOfType(int type)
	{
		switch ((NPCDetails.NPCAppearance.AppearanceType)type)
		{
			case NPCDetails.NPCAppearance.AppearanceType.Skin:
				return skinColors[Random.Range(0, skinColors.Length)];
			case NPCDetails.NPCAppearance.AppearanceType.Hair:
				return hairColors[Random.Range(0, hairColors.Length)];
			case NPCDetails.NPCAppearance.AppearanceType.Shirt:
				return shirtColors[Random.Range(0, shirtColors.Length)];
			case NPCDetails.NPCAppearance.AppearanceType.Pants:
				return pantsColors[Random.Range(0, pantsColors.Length)];
			case NPCDetails.NPCAppearance.AppearanceType.Shoes:
				return shoeColors[Random.Range(0, shoeColors.Length)];
			default:
				return null;
		}
	}

}
