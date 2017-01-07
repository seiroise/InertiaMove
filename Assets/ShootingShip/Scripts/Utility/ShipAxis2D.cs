using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingShip.Utility {

	/// <summary>
	/// 機体の回転軸
	/// </summary>
	public class ShipAxis2D : MonoBehaviour {

		[SerializeField, Range(0f, 120f)]
		private float steering = 20f;
		[SerializeField]
		private bool isLocal = true;
		public bool IsLocal { set { isLocal = value; } get { return isLocal; } }
		[SerializeField, ContextMenuItem("Set", "SetTargetAngle")]
		private float targetAngle;

		private bool isLerping = false;			//補間中
		private float epsilon = 0.01f;			//終了閾値
		private Vector3 tAngles = Vector3.zero;	//一時
		public float AngleZ { get { return tAngles.z; } }

		#region UnityEvent

		private void Update() {
			if(isLerping) UpdateAngle();
		}

		#endregion

		#region Function

		/// <summary>
		/// 回転
		/// </summary>
		private void UpdateAngle() {
			float delta;
			if (isLocal) {
				tAngles.z = transform.localEulerAngles.z;
				tAngles.z = Mathf.LerpAngle(tAngles.z, targetAngle, steering * Time.deltaTime);
				transform.localEulerAngles = tAngles;
			} else {
				tAngles.z = transform.eulerAngles.z;
				tAngles.z = Mathf.LerpAngle(tAngles.z, targetAngle, steering * Time.deltaTime);
				transform.eulerAngles = tAngles;
			}
			delta = Mathf.Abs(targetAngle - tAngles.z);
			if(delta < epsilon) {
				isLerping = false;
			}
		}

		/// <summary>
		/// 角度を設定
		/// </summary>
		public void SetAngle(float angle) {
			targetAngle = angle;
			isLerping = true;
		}

		/// <summary>
		/// 角度を設定
		/// </summary>
		private void SetTargetAngle() {
			SetAngle(targetAngle);
		}

		#endregion
	}
}