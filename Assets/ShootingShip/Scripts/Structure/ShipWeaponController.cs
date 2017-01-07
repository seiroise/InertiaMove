using UnityEngine;
using System;
using System.Collections.Generic;

namespace ShootingShip.Structure {

	/// <summary>
	/// 機体の武器の操作
	/// </summary>
	public class ShipWeaponController : ShipComManager<ShipWeaponHolder> {

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(ShipStructure structure) {
			base.InitCom(structure);
			structure.WeaponController = this;
		}

		#endregion

		#region Function

		/// <summary>
		/// 弾に設定するタグの設定
		/// </summary>
		public void SetBulletExclusionTag(string tag) {
			foreach(var c in coms) {
				c.Equipment.BulletExclusionTag = tag;
			}
		}

		/// <summary>
		/// 角度の設定
		/// </summary>
		public void SetAngle(float angle) {
			foreach (var c in coms) {
				c.SetAngle(angle);
			}
		}

		/// <summary>
		/// 目標との角度を設定
		/// </summary>
		public void SetTargetAngle(Transform target) {
			foreach (var c in coms) {
				c.SetTargetAngle(target);
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
			foreach (var c in coms) {
				c.Axis.IsLocal = true;
				c.Equipment.WeaponStandby();
			}
		}

		#endregion
	}
}