using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InertiaShooting {

	/// <summary>
	/// 2次元での回転
	/// </summary>
	public class Rotation2D : MonoBehaviour {

		[SerializeField, Range(0f, 120f)]
		private float steering = 60f;
		[SerializeField]
		private bool isLocal = true;
		public bool IsLocal { set { isLocal = value; } }
		[SerializeField]
		private float targetAngle;

		#region UnityEvent

		private void Update() {
			UpdateAngle();
		}

		#endregion

		#region Function

		/// <summary>
		/// 回転
		/// </summary>
		private void UpdateAngle() {
			if (isLocal) {
				float angle = Mathf.LerpAngle(transform.localEulerAngles.z, targetAngle, steering * Time.deltaTime);
				transform.localEulerAngles = new Vector3(0f, 0f, angle);
			} else {
				float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, steering * Time.deltaTime);
				transform.eulerAngles = new Vector3(0f, 0f, angle);
			}
		}

		/// <summary>
		/// 角度を設定
		/// </summary>
		public void SetAngle(float angle) {
			targetAngle = angle;
		}

		#endregion
	}
}