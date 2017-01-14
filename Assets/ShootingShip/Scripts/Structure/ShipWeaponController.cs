using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Equipment;

namespace ShootingShip.Structure {

	/// <summary>
	/// 機体の武器の操作
	/// </summary>
	public class ShipWeaponController : ShipEquipmentController<ShipWeapon, ShipWeaponHolder> {

		//ターゲッティング
		private Transform trans;
		private Rigidbody2D targetRigid;	//目標

		#region UnityEvent

		private void Update() {
			Targeting();
		}

		#endregion

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(ShipStructure structure) {
			base.InitCom(structure);
			trans = transform;
			structure.WeaponController = this;
		}

		#endregion

		#region Function

		/// <summary>
		/// ターゲッティング(偏差)
		/// </summary>
		private void Targeting() {
			if (!targetRigid) return;
			Vector3 tPos = targetRigid.position;
			Vector3 delta = targetRigid.velocity;
			float distance = (tPos - trans.position).magnitude;
			//目標角度の設定
			foreach (var c in coms) {
				c.SetTargetAngle(tPos + delta * (distance / c.Equipment.BaseSpeed));
			}
		}

		/// <summary>
		/// 弾に設定するタグの設定
		/// </summary>
		public void SetBulletExclusionTag(string tag) {
			foreach(var c in coms) {
				c.Equipment.BulletExclusionTag = tag;
			}
		}

		/// <summary>
		/// 目標との角度を設定
		/// </summary>
		public void SetTargetAngle(Vector3 wPosition) {
			foreach (var c in coms) {
				c.SetTargetAngle(wPosition);
			}
		}

		/// <summary>
		/// 目標の設定
		/// </summary>
		public void SetTarget(Rigidbody2D target) {
			if (targetRigid != target) {
				targetRigid = target;
			}
		}

		/// <summary>
		/// 全ての武器を起動
		/// </summary>
		public void WeaponAwake() {
			foreach (var c in coms) {
				c.Axis.IsLocal = false;
				c.Equipment.WeaponAwake();
			}
		}

		/// <summary>
		/// 全ての武器を待機
		/// </summary>
		public void WeaponStandby() {
			SetTarget(null);
			foreach (var c in coms) {
				c.Axis.IsLocal = true;
				c.SetAngle(0f);
				c.Equipment.WeaponStandby();
			}
		}

		#endregion
	}
}