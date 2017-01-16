using UnityEngine;
using System;
using System.Collections;

namespace EditorUtility {

	/// <summary>
	/// 固定値属性
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public class DisableAttribute : PropertyAttribute { }
}