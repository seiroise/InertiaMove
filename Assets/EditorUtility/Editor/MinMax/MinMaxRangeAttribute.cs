﻿using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace EditorUtility {

	/// <summary>
	/// 最小値と最大値の範囲を指定する属性
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public class MinMaxRangeAttribute : PropertyAttribute {

		public readonly float minLimit;
		public readonly float maxLimit;

		public MinMaxRangeAttribute(float minLimit = 0f, float maxLimit = 1f) {
			this.minLimit = minLimit;
			this.maxLimit = maxLimit;
		}
	}
}