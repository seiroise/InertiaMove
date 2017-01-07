using UnityEngine;
using System.Collections;
using ShootingUtility.ObjectPool;
using System;
using ShootingUtility.Lerp;

namespace ShootingUtility.ValueIndicator {

	/// <summary>
	/// 0-1のfloat値の表示器
	/// frontはbackの子関係にあること
	/// </summary>
	public class FloatIndicator : MonoBehaviour, IPoolable {

		[SerializeField]
		private Transform front;
		[SerializeField]
		private Transform back;
		[SerializeField]
		private Vector3 baseScale = new Vector3(5f, 1f, 1f);
		private Vector3 tempScale;

		[Header("Tracker")]
		[SerializeField]
		private LerpTracker tracker;
		public LerpTracker Tracker { get { return tracker; } }

		private float baseValue;

		#region Function

		/// <summary>
		/// 割合の設定
		/// </summary>
		public void SetRatio(float baseValue, float value) {
			SetRatio(value / baseValue);
		}

		/// <summary>
		/// 割合の設定
		/// </summary>
		public void SetRatio(float ratio) {
			tempScale.x = ratio;
			front.localScale = tempScale;
		}

		#endregion

		#region IPoolable

		/// <summary>
		/// 初期化
		/// </summary>
		public void InitPoolable() {
			tempScale = Vector3.one;
			back.localScale = baseScale;
			front.localScale = tempScale;
		}

		#endregion
	}
}