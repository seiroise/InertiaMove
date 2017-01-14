using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Equipment;

namespace ShootingShip.Structure {
	
	/// <summary>
	/// 機体の推進器の操作
	/// </summary>
	public class ShipThrusterController : ShipEquipmentController<ShipThruster, ShipThrusterHolder> {

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(ShipStructure structure) {
			base.InitCom(structure);
			structure.ThrusterController = this;
		}

		#endregion

		#region Function

		/// <summary>
		/// 全ての推進器の角度を設定
		/// </summary>
		public void SetThrusterAngle(float angle) {
			foreach (var c in coms) {
				c.Axis.SetAngle(angle);
			}
		}

		/// <summary>
		/// 全ての推進器の効率限界を設定(0~1)
		/// </summary>
		public void SetEfficiency(float efficiency) {
			foreach (var c in coms) {
				c.Equipment.SetEfficiency(efficiency);
			}
		}

		/// <summary>
		/// 全ての推進器を起動
		/// </summary>
		public void ThrusterAwake() {
			foreach (var c in coms) {
				c.Equipment.ThrusterAwake();
			}
		}

		/// <summary>
		/// 全ての推進器を待機
		/// </summary>
		public void ThrusterStandby() {
			foreach (var c in coms) {
				c.Equipment.ThrusterStandby();
			}
		}

		#endregion
	}
}