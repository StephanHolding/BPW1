using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldoutAttribute : PropertyAttribute
{

	public string title;

	public FoldoutAttribute(string title)
	{
		this.title = title;
	}
}
