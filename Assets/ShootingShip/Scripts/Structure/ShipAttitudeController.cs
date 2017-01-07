using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Utility;

namespace ShootingShip.Structure {

	/// <summary>
	/// 機体の姿勢制御器
	/// </summary>
	public class ShipAttitudeController : ShipCom {

		[Header("調整用")]
		[SerializeField, Range(0.01f, 1f)]
		private float steeringAdjust = 0.2f;
		
		[Header("補助用")]
		[SerializeField]
		private bool rotationRigidbody = true;
		[SerializeField]
		private ShipAxis2D shipAxis;

		private ShipThrusterController thruster;
		private Rigidbody2D rBody2d;

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(ShipStructure structure) {
			base.InitCom(structure);
			structure.AttitudeController = this;
		}

		/// <summary>
		/// 起動
		/// </summary>
		public override void AwakeCom() {
			base.AwakeCom();
			thruster = structure.ThrusterController;
			rBody2d = structure.RBody2d;
		}

		#endregion

		#region Function

		/// <summary>
		/// 指定した方向に進む
		/// </summary>
		public void Go(float angle) {
			if (shipAxis) {
				shipAxis.SetAngle(angle);
			}
		}

		/// <summary>
		/// 回転
		/// </summary>
		private void Rotation(float delta) {
			//スラスターの方向制御
			thruster.SetThrusterAngle(-(delta - rBody2d.angularVelocity * steeringAdjust));
		}

		#endregion

	}
}