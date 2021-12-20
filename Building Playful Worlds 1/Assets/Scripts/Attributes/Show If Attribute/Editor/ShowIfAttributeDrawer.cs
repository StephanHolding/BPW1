using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ShowByEnumAttribute))]
public class ShowByEnumAttributeDrawer : PropertyDrawer
{

	private bool drawProperty;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		ShowByEnumAttribute showByEnum = attribute as ShowByEnumAttribute;

		SerializedProperty enumInstance = property.serializedObject.FindProperty(showByEnum.nameOfEnumInstance);

		if (enumInstance != null)
		{
			if (!string.IsNullOrEmpty(showByEnum.labelOverride))
				label.text = showByEnum.labelOverride;

			drawProperty = ShouldDrawProperty(showByEnum.enumValues, enumInstance.enumValueIndex);
		}
		else
		{
			drawProperty = true;
		}

		if (drawProperty)
		{
			EditorGUI.PropertyField(position, property, label, true);
		}
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		if (drawProperty)
			return base.GetPropertyHeight(property, label);
		else
			return 0;
	}

	private bool ShouldDrawProperty(int[] enumValues, int enumInstanceValues)
	{
		for (int i = 0; i < enumValues.Length; i++)
		{
			if (enumValues[i] == enumInstanceValues)
			{
				return true;
			}
		}

		return false;
	}

}

[CustomPropertyDrawer(typeof(ShowByBoolAttribute))]
public class ShowByBoolAttributeDrawer : PropertyDrawer
{

	private bool drawProperty;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		ShowByBoolAttribute showByBool = attribute as ShowByBoolAttribute;

		SerializedProperty boolInstance = property.serializedObject.FindProperty(showByBool.nameOfBoolInstance);

		if (boolInstance != null)
		{
			if (!string.IsNullOrEmpty(showByBool.labelOverride))
				label.text = showByBool.labelOverride;

			drawProperty = boolInstance.boolValue == showByBool.boolValue;
		}
		else
		{
			drawProperty = true;
		}

		if (drawProperty)
			EditorGUI.PropertyField(position, property, label, true);
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		if (drawProperty)
			return base.GetPropertyHeight(property, label);
		else
			return 0;
	}
}
