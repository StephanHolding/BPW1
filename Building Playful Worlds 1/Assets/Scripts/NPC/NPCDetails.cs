
using System.Collections.Generic;

[System.Serializable]
public class NPCDetails 
{
	[System.Serializable]
	public class NPCAppearance
	{
		public enum AppearanceType
		{
			Skin,
			Hair,
			Shirt,
			Pants,
			Shoes
		}

		public AppearanceType appearanceType;
		public NamedColor.NamedColorEnum appearanceColor;

		public NPCAppearance(AppearanceType appearanceType, NamedColor.NamedColorEnum appearanceColor)
		{
			this.appearanceType = appearanceType;
			this.appearanceColor = appearanceColor;
		}

		public NPCAppearance(int appearanceType, NamedColor.NamedColorEnum appearanceColor)
		{
			this.appearanceType = (AppearanceType)appearanceType;
			this.appearanceColor = appearanceColor;
		}
	}

	public enum DetailType
	{
		Name,
		Gender,
		Age,
		PlaceOfResidence,
		Job,
		Appearance,
		SpecialDetail
	}

	public string NPC_name;
	public string NPC_gender;
	public int NPC_age;
	public string NPC_place;
	public string NPC_job;
	public NPCAppearance[] NPC_appearances;
	public string NPC_specialDetail;

	public bool Compare(NPCDetails other)
	{
		if (NPC_name != other.NPC_name)
			return false;

		if (NPC_gender != other.NPC_gender)
			return false;

		if (NPC_age != other.NPC_age)
			return false;

		if (NPC_place != other.NPC_place)
			return false;

		if (NPC_job != other.NPC_job)
			return false;

		if (NPC_specialDetail != other.NPC_specialDetail)
			return false;

		for (int i = 0; i < NPC_appearances.Length; i++)
		{
			if (NPC_appearances[i].appearanceColor != other.NPC_appearances[i].appearanceColor)
				return false;
		}

		return true;
	}
}
