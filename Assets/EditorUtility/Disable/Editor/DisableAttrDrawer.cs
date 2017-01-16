using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

namespace EditorUtility {

	/// <summary>
	/// 固定値属性の描画
	/// </summary>
	[CustomPropertyDrawer(typeof(DisableAttribute))]
	public class DisableAttributeDrawer : PropertyDrawer {

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

			EditorGUI.BeginDisabledGroup(true);
			EditorGUI.PropertyField(position, property, label);
			EditorGUI.EndDisabledGroup();
		}
	}
}