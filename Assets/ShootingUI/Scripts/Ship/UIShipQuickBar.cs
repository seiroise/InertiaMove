using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Structure;

namespace ShootingUI.Ship {

	/// <summary>
	/// 機体の武器と推進器のクイックバー
	/// </summary>
	public class UIShipQuickBar : MonoBehaviour {

		[SerializeField]
		private UIWeaponQuickBar weaponQuickBar;
		[SerializeField]
		private UIThrusterQuickBar thrusterQuickBar;

		#region Function

		/// <summary>
		/// 武器と推進器の設定
		/// </summary>
		public void SetQuickBar(ShipStructure structure) {
			if (weaponQuickBar) {
				weaponQuickBar.SetEquipments(structure.WeaponController);
			}
			if (thrusterQuickBar) {
				thrusterQuickBar.SetEquipments(structure.ThrusterController);
			}
		}

		#endregion
	}
}