using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Equipment;

namespace ShootingShip.Structure {

	/// <summary>
	/// 機体の武器ホルダー
	/// </summary>
	public class ShipWeaponHolder : ShipEquipmentHolder<ShipWeapon> {

		#region Function

		/// <summary>
		/// 目標との角度の設定
		/// </summary>
		public void SetTargetAngle(Transform target) {
			SetTargetAngle(target.position);
		}

		/// <summary>
		/// 目標との角度の設定
		/// </summary>
		public void SetTargetAngle(Vector3 wPosition) {
			if (axis) {
				Vector3 d = wPosition - transform.position;
				axis.SetAngle(Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg);
			}
		}

		#endregion

	}
}