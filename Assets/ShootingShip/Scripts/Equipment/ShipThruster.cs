using UnityEngine;
using System;
using System.Collections.Generic;

namespace ShootingShip.Equipment {
	
	/// <summary>
	/// 機体の推進器
	/// </summary>
	public class ShipThruster : ShipEquipment {

		[Header("加速")]
		[SerializeField, Range(0.01f, 100f)]
		private float accel = 10f;

		[Header("減速")]
		[SerializeField, Range(0.01f, 10f)]
		private float deccel = 2f;

		[Header("温度")]
		[SerializeField, Range(0f, 10f)]
		private float warmness = 4f;
		[SerializeField, Range(0f, 10f)]
		private float coolness = 4f;
		private float temp = 0f;			//温度
		private const float MAX_TEMP = 10f;	//最大温度
		private float efficiency = 1f;		//効率(0~1)

		[Header("アニメーション")]
		[SerializeField]
		private Animator animator;
		[SerializeField]
		private string thrustAnim;

		[Header("エフェクト")]
		[SerializeField]
		private Gradient burnerColor;
		[SerializeField]
		private ParticleSystem thrustEffect;

		private bool isThrust;
		private Rigidbody2D rBody2d;	//重心剛体
		private Vector2 addVelocity;	//加速度

		#region UnityEvent

		private void FixedUpdate() {
			Combust();
			Thrust();
		}

		#endregion

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(Structure.ShipStructure structure) {
			base.InitCom(structure);

		}

		/// <summary>
		/// 起動
		/// </summary>
		public override void AwakeCom() {
			base.AwakeCom();
			SetRigidbody(structure.RBody2d);
		}

		#endregion

		#region Function

		/// <summary>
		/// 加速対象の剛体を設定
		/// </summary>
		public void SetRigidbody(Rigidbody2D rBody2d) {
			this.rBody2d = rBody2d;
		}

		#endregion

		#region Thrust

		/// <summary>
		/// 燃焼
		/// </summary>
		private void Combust() {
			//燃焼
			if (isThrust) {
				if (temp < MAX_TEMP) {
					temp += warmness * Time.deltaTime;
					if (temp >= MAX_TEMP) {
						temp = MAX_TEMP;
					}
				}
			} else {
				if (temp > 0f) {
					temp -= coolness * Time.deltaTime;
					if (temp <= 0f) {
						temp = 0f;
					}
				}
			}
			efficiency = temp / MAX_TEMP;
		}

		/// <summary>
		/// 推進
		/// </summary>
		private void Thrust() {
			//加減速
			if (isThrust) {
				if (rBody2d && efficiency > 0f) {
					addVelocity = transform.right * accel * (2f - efficiency);
					rBody2d.velocity += addVelocity * Time.deltaTime;

				}
			} else {
				if (rBody2d && efficiency > 0f) {
					rBody2d.velocity -= Vector2.Lerp(rBody2d.velocity, Vector2.zero, deccel) * Time.deltaTime;
				}
			}

			if (thrustEffect && efficiency > 0f) {
				thrustEffect.Emit(
					position: thrustEffect.transform.position,
					velocity: -addVelocity * 0.4f,
					size: efficiency + 0.5f,
					lifetime: UnityEngine.Random.Range(efficiency, efficiency + 1f) * 0.4f,
					color: burnerColor.Evaluate(efficiency));
			}
		}

		/// <summary>
		/// 燃焼効率の設定(0~1)
		/// </summary>
		public void SetEfficiency(float efficiency) {
			efficiency = Mathf.Clamp01(efficiency);
		}

		/// <summary>
		/// 推進器起動
		/// </summary>
		public void ThrusterAwake() {
			isThrust = true;
		}

		/// <summary>
		/// 推進器待機
		/// </summary>
		public void ThrusterStandby() {
			isThrust = false;
		}

		#endregion
	}
}