using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowByEnumAttribute : PropertyAttribute
{

	public int[] enumValues;
	public string nameOfEnumInstance;
	public string labelOverride;

	/// <summary>
	/// Show a variable in the inspector depending on the value of an enum.
	/// </summary>
	/// <param name="enumValues">The variable is shown when the enum is one of these values. Represented by an int</param>
	/// <param name="nameOfEnumInstance">Name of the enum instance. Used to retrieve the value of the enum at runtime.</param>
	public ShowByEnumAttribute(string nameOfEnumInstance, params int[] enumValues)
	{
		this.enumValues = enumValues;
		this.nameOfEnumInstance = nameOfEnumInstance;
		this.labelOverride = string.Empty;
	}

	/// <summary>
	/// Show a variable in the inspector depending on the value of an enum.
	/// </summary>
	/// <param name="enumValues">The variable is shown when the enum is one of these values. Represented by an int</param>
	/// <param name="nameOfEnumInstance">Name of the enum instance. Used to retrieve the value of the enum at runtime.</param>
	/// <param name="labelOverride">Name of the variable in the inspector.</param>
	public ShowByEnumAttribute(string nameOfEnumInstance, string labelOverride, params int[] enumValues)
	{
		this.enumValues = enumValues;
		this.nameOfEnumInstance = nameOfEnumInstance;
		this.labelOverride = labelOverride;
	}

	/// <summary>
	/// Show a variable in the inspector depending on the value of an enum.
	/// </summary>
	/// <param name="enumValues">The variable is shown when the enum is this value. Represented by an int</param>
	/// <param name="nameOfEnumInstance">Name of the enum instance. Used to retrieve the value of the enum at runtime.</param>
	public ShowByEnumAttribute(int enumValue, string nameOfEnumInstance)
	{
		this.enumValues = new int[] { enumValue };
		this.nameOfEnumInstance = nameOfEnumInstance;
		this.labelOverride = string.Empty;
	}

	/// <summary>
	/// Show a variable in the inspector depending on the value of an enum.
	/// </summary>
	/// <param name="enumValues">The variable is shown when the enum is this value. Represented by an int</param>
	/// <param name="nameOfEnumInstance">Name of the enum instance. Used to retrieve the value of the enum at runtime.</param>
	/// <param name="labelOverride">Name of the variable in the inspector.</param>
	public ShowByEnumAttribute(int enumValue, string nameOfEnumInstance, string labelOverride)
	{
		this.enumValues = new int[] { enumValue };
		this.nameOfEnumInstance = nameOfEnumInstance;
		this.labelOverride = labelOverride;
	}
}

public class ShowByBoolAttribute : PropertyAttribute
{
	public bool boolValue;
	public string nameOfBoolInstance;
	public string labelOverride;


	public ShowByBoolAttribute(bool boolValue, string nameOfBoolInstance)
	{
		this.boolValue = boolValue;
		this.nameOfBoolInstance = nameOfBoolInstance;
	}

	public ShowByBoolAttribute(bool boolValue, string nameOfBoolInstance, string labelOverride)
	{
		this.boolValue = boolValue;
		this.nameOfBoolInstance = nameOfBoolInstance;
		this.labelOverride = labelOverride;
	}
}
