using UnityEngine;
using System;
using STG.Obj.Equipment;

namespace STG.Obj.Thruster {

	/// <summary>
	/// STG用の推進器
	/// </summary>
	public class STGObjThruster : STGObjEquipment {

		[Header("パラメータ")]
		[SerializeField]
		private STGObjThrusterParameter baseParameter;

		[Header("エフェクト")]
		[SerializeField]
		private ParticleSystem thrustEffect;

		private Rigidbody2D thrustRigidbody2d;  //推進させる剛体
		public Rigidbody2D ThrustRigidbody2D { get { return thrustRigidbody2d; } set { 
				thrustRigidbody2d = value; } }

		private float combustEfficiency;        //燃焼効率
		public float CombustEfficiency {
			set {
				if(value > baseParameter.MaxCombustEfficiency) {
					value = baseParameter.MaxCombustEfficiency;
				}
				combustEfficiency = value;
			}
		}

		private Vector2 addVelocity;

		#region UnityEvent

		private void FixedUpdate() {
			Thrust();
		}

		#endregion

		#region Function

		/// <summary>
		/// 推進
		/// </summary>
		private void Thrust() {
			if(!thrustRigidbody2d) return;
			if(combustEfficiency <= 0.1f) return;
			addVelocity = transform.right * baseParameter.Accele * combustEfficiency * Time.deltaTime;
			thrustRigidbody2d.velocity += addVelocity;
		}

		#endregion
	}
}