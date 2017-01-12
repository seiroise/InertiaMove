using UnityEngine;
using UnityEngine.UI;
using System;
using ShootingShip.Equipment;

namespace ShootingUI.Ship {

	/// <summary>
	/// 機体の武器の表示
	/// </summary>
	public class UIShipWeaponIndicator : UIShipEquipmentIndicator<ShipWeapon> {

		#region VirtualFunction

		/// <summary>
		/// 装備の設定
		/// </summary>
		public override void SetEquipment(ShipWeapon equipment) {
			
		}

		/// <summary>
		/// 装備の解除
		/// </summary>
		public override void RemoveEquipment() {
			
		}

		#endregion
	}
}