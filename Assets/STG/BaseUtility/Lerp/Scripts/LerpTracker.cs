using UnityEngine;
using System;
using System.Collections.Generic;

namespace STG.BaseUtility.Lerp {

	/// <summary>
	/// 線形補間しながら追跡
	/// </summary>
	[AddComponentMenu("STG/BaseUtility/Lerp/LerpTracker")]
	public class LerpTracker : MonoBehaviour {

		/// <summary>
		/// 更新タイプ
		/// </summary>
		public enum UpdateType {
			Normal,
			Late,
			Fixed
		}

		[SerializeField]
		private Transform target;
		public Transform Target { set { target = value; } }
		[SerializeField]
		private Vector3 offset;
		[SerializeField, Range(0.1f, 120f)]
		private float amount = 60f;

		[SerializeField]
		private UpdateType updateType;

		#region UnityEvent

		private void Update() {
			if (updateType == UpdateType.Normal) Move();
		}

		private void LateUpdate() {
			if (updateType == UpdateType.Late) Move();
		}

		private void FixedUpdate() {
			if (updateType == UpdateType.Fixed) Move();
		}

		#endregion

		#region Function

		/// <summary>
		/// 移動
		/// </summary>
		private void Move() {
			if (target) {
				transform.position = Vector3.Lerp(transform.position, target.position + offset, amount * Time.deltaTime);
			}
		}
		#endregion
	}
}