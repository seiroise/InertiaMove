using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace EditorUtility {

	/// <summary>
	/// 最大値と最小値を保持するクラス
	/// </summary>
	[Serializable]
	public struct MinMax {

		public float min;
		public float max;

		public float randomValue { get { return UnityEngine.Random.Range(min, max); } }

		public MinMax(float min, float max) {
			this.min = min;
			this.max = max;
		}

		public float Clamp(float value) {
			return Mathf.Clamp(value, min, max);
		}
	}
}