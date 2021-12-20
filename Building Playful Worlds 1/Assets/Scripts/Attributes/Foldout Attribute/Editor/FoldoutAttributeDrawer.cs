using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(FoldoutAttribute))]
public class FoldoutAttributeDrawer : PropertyDrawer
{

	private bool expanded;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		FoldoutAttribute foldout = attribute as FoldoutAttribute;
		expanded = EditorGUI.Foldout(position, true, foldout.title);

		if (expanded)
		{
			EditorGUI.PropertyField(position, property);
		}
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		if (expanded)
			return base.GetPropertyHeight(property, label);
		else
			return 0;
	}
}
