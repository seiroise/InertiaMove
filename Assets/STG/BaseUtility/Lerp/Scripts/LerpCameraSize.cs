using UnityEngine;
using System;
using System.Collections.Generic;

namespace STG.BaseUtility.Lerp {

	/// <summary>
	/// カメラサイズの補間
	/// </summary>
	[AddComponentMenu("STG/BaseUtility/Lerp/LerpCameraSize")]
	public class LerpCameraSize : MonoBehaviour {

		[SerializeField]
		private Camera target;
		[SerializeField, Range(0.1f, 200f)]
		private float targetSize = 5f;
		[SerializeField, Range(0.1f, 120f)]
		private float amount = 60f;

		#region UnityEvent

		private void Update() {
			UpdateCameraSize();
		}

		#endregion

		#region Function

		/// <summary>
		/// カメラサイズの更新
		/// </summary>
		private void UpdateCameraSize() {
			if (target) {
				target.orthographicSize = Mathf.Lerp(target.orthographicSize, targetSize, amount * Time.deltaTime);
			}
		}

		/// <summary>
		/// カメラサイズを設定
		/// </summary>
		public void SetCameraSize(float size) {
			targetSize = size;
		}

		#endregion
	}
}