using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingUtility.ObjectDetector;

namespace InertiaShooting {

	/// <summary>
	/// Playerと倒す存在
	/// </summary>
	public class Enemy : Character {

		[Header("検出関連")]
		[SerializeField]
		private DetectableObject2D detectObj;

		#region UnityEvent

		private void Start() {
			InitPoolable();
		}

		#endregion

		#region IPoolable

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitPoolable() {
			base.InitPoolable();

			if (detectObj) {
				detectObj.OnDetected.RemoveAllListeners();
				detectObj.OnDetected.AddListener(OnDetected);
				detectObj.OnReleased.RemoveAllListeners();
				detectObj.OnReleased.AddListener(OnReleased);
			}
		}

		#endregion

		#region Callback

		/// <summary>
		/// 検出された
		/// </summary>
		private void OnDetected(ObjectDetector2D detector) {
			
		}

		/// <summary>
		/// 解除された
		/// </summary>
		private void OnReleased(ObjectDetector2D detector) {
			
		}

		#endregion
	}
}