using UnityEngine;
using System;
using System.Collections.Generic;

namespace ShootingUtility.Lerp {

	/// <summary>
	/// 線形補間で透明度を変更
	/// </summary>
	public class LerpSpriteAlpha : MonoBehaviour {

		[SerializeField]
		private SpriteRenderer target;
		[SerializeField, Range(0f, 1f)]
		private float targetAlpha = 0.5f;
		[SerializeField, Range(0.1f, 120f)]
		private float amount = 60f;

		private bool isLerped;
		private readonly float epsilon = 0.001f;

		private Color temp;

		#region UnityEvent

		private void Update() {
			if (isLerped) {
				UpdateSpriteAlpha();
			}
		}

		#endregion

		#region Function

		/// <summary>
		/// 透明度の更新
		/// </summary>
		private void UpdateSpriteAlpha() {
			if (target) {
				temp.a = Mathf.Lerp(temp.a, targetAlpha, amount * Time.deltaTime);
				if (Mathf.Abs(targetAlpha - temp.a) < epsilon) {
					temp.a = targetAlpha;
					isLerped = false;
				}
				target.color = temp;
			}
		}

		/// <summary>
		/// 透明度の設定
		/// </summary>
		public void SetSpriteAlpha(float alpha) {
			targetAlpha = Mathf.Clamp01(alpha);
			temp = target.color;
			isLerped = true;
		}

		/// <summary>
		/// 透明度の強制
		/// </summary>
		public void ForceSpriteAlpha(float alpha) {
			temp = target.color;
			temp.a = Mathf.Clamp01(alpha);
			target.color = temp;
		}

		#endregion
	}
}