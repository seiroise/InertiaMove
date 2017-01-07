using UnityEngine;
using System;
using System.Collections.Generic;

namespace InertiaShooting {
	
	/// <summary>
	/// Enemyのコントローラ
	/// </summary>
	public class EnemyController : MonoBehaviour{

		[Header("物理")]
		[SerializeField]
		private Rigidbody2D rBody2D;

		[Header("加速")]
		[SerializeField, Range(0f, 1000f)]
		private float maxSpeed;     //最大速度
		[SerializeField, Range(0f, 1000f)]
		private float Thrust;       //推進力
		[SerializeField, Range(0f, 1000f)]
		private float quickThrust;  //クイック推進力

		[Header("旋回")]
		[SerializeField, Range(0f, 60f)]
		private float steering = 10f;

		private float targetAngle;

		#region UnityEvent

		private void Update() {
			float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, steering * Time.deltaTime);
			transform.eulerAngles = new Vector3(0f, 0f, angle);
		}

		#endregion

		#region Function

		/// <summary>
		/// 角度の設定
		/// </summary>
		public void SetAngle(float angle) {
			
		}

		/// <summary>
		/// 加速
		/// </summary>
		public void AddVelocity(Vector2 dir) {
			
		}

		/// <summary>
		/// 速度限界を考慮せず加速する
		/// </summary>
		public void AddVelocityNoLimit(Vector2 dir) {
			
		}

		#endregion

	}
}