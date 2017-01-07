using UnityEngine;
using System;
using System.Collections.Generic;

namespace ShootingShip.Equipment {
	
	/// <summary>
	/// 機体の推進器
	/// </summary>
	public class ShipThruster : ShipEquipment {

		[Header("推進")]
		[SerializeField, Range(0.01f, 100f)]
		private float thrust = 10f;			//推力
		[SerializeField]
		private AnimationCurve steeringEfficiency = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);	//旋回時の出力効率
		private float efficiency = 1f;		//効率(0~1)
		private bool isThrust;

		[Header("溜め")]
		[SerializeField, Range(0.01f, 1f)]
		private float chargeThreshold = 0.2f;
		private float charge;
		[SerializeField, Range(1f, 2000f)]
		private float chargeBoost = 100f;

		[Header("旋回")]
		[SerializeField, Range(1f, 10f)]
		private float steering = 1f;

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
		[SerializeField, Range(0f, 10f)]
		private float warmness = 4f;
		[SerializeField, Range(0f, 10f)]
		private float coolness = 4f;
		private float temperature = 0f;
		private float maxTemperature = 10f;

		private Rigidbody2D rBody2d;	//重心剛体
		private Vector2 centerDis;		//重心までの距離

		#region UnityEvent

		private void FixedUpdate() {
			Combust();
		}

		#endregion

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(Structure.ShipStructure structure) {
			base.InitCom(structure);
			charge = 0f;
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
			if (!isThrust) return;
			float delta = Mathf.Clamp(Mathf.DeltaAngle(transform.eulerAngles.z, rBody2d.transform.eulerAngles.z), -90f, 90f);
			float sEfficiency = steeringEfficiency.Evaluate((1f - Mathf.Abs(delta) / 90f));
			Charge(sEfficiency);
			//加速度
			Vector2 velocity = -transform.right * thrust * efficiency;
			float vMag = velocity.magnitude;
			//剛体の加速と回転
			if (rBody2d) {
				//角速度
				float angulerVelocity = (vMag * delta * 0.1f * steering) - (rBody2d.angularVelocity * 0.2f * steering);
				rBody2d.angularVelocity += angulerVelocity * Time.deltaTime;
				//速度
				rBody2d.velocity += -velocity * Time.deltaTime * sEfficiency;
			}
			//エフェクト生成
			if (thrustEffect) {
				thrustEffect.Emit(
					position: thrustEffect.transform.position,
					velocity: velocity * 0.4f,
					size: (efficiency + 1.5f - sEfficiency) * 2f,
					lifetime: UnityEngine.Random.Range(efficiency, efficiency + sEfficiency) * 0.4f,
					color: burnerColor.Evaluate(sEfficiency));
			}
		}

		/// <summary>
		/// チャージ
		/// </summary>
		private void Charge(float sEfficiency) {
			if(sEfficiency < chargeThreshold) {
				charge += sEfficiency * 10f * Time.deltaTime;
				if(charge > 1f) charge = 1f;
			} else {
				charge -= sEfficiency * 20f * Time.deltaTime;
				if(charge < 0f) charge = 0f;
			}
		}

		/// <summary>
		/// チャージブースト
		/// </summary>
		public void ChargeBoost() {
			rBody2d.AddForce(rBody2d.transform.right);
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