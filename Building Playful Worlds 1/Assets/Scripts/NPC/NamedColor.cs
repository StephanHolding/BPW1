using UnityEngine;
[System.Serializable]
public static class NamedColor
{
	public enum NamedColorEnum
	{
		Red,
		Blue,
		Green,
		Black,
		White,
		Purple,
		Yellow,
		Orange,
		Brown,
		Pink,
	}

	public static Color NamedColorToRGB(NamedColorEnum namedColor)
	{
		switch (namedColor)
		{
			case NamedColorEnum.Red:
				return RGBToRGBDecimal(255, 0, 0);
			case NamedColorEnum.Blue:
				return RGBToRGBDecimal(0, 0, 255);
			case NamedColorEnum.Green:
				return RGBToRGBDecimal(0, 255, 0);
			case NamedColorEnum.Black:
				return RGBToRGBDecimal(0, 0, 0);
			case NamedColorEnum.White:
				return RGBToRGBDecimal(255, 255, 255);
			case NamedColorEnum.Purple:
				return RGBToRGBDecimal(148, 0, 211);
			case NamedColorEnum.Yellow:
				return RGBToRGBDecimal(255, 255, 0);
			case NamedColorEnum.Orange:
				return RGBToRGBDecimal(255, 128, 0);
			case NamedColorEnum.Brown:
				return RGBToRGBDecimal(102, 51, 0);
			case NamedColorEnum.Pink:
				return RGBToRGBDecimal(255, 102, 255);
			default:
				return Color.clear;
		}
	}

	public static Color RGBToRGBDecimal(int r, int g, int b, int a = 255)
	{
		return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
	}

	public static Color NamedColorToSkinColor(NamedColorEnum namedColor)
	{
		string hexToParse = string.Empty;

		switch (namedColor)
		{
			case NamedColorEnum.Black:
				hexToParse = "#2E1408";
				break;
			case NamedColorEnum.White:
				hexToParse = "#D2B28E";
				break;
			case NamedColorEnum.Yellow:
				hexToParse = "#B99B6A";
				break;
			case NamedColorEnum.Brown:
				hexToParse = "#412D15";
				break;
		}

		if (ColorUtility.TryParseHtmlString(hexToParse, out Color toReturn))
		{
			return toReturn;
		}
		else
		{
			Debug.LogError("Skin color parse was unsuccessful");
			return Color.red;
		}
	}

}
