using UnityEngine;
using UnityEngine.UI;
using System;
using ShootingShip.Equipment;

namespace ShootingUI.Ship {

	/// <summary>
	/// 機体の武器の表示
	/// </summary>
	public class UIWeaponIndicator : UIEquipmentIndicator<ShipWeapon> {

		[Header("Reload Bar")]
		[SerializeField]
		private Image reloadBar;

		#region UnityEvent

		private void Update() {
			UpdateReloadBar();
		}

		#endregion

		#region Function

		/// <summary>
		/// リロードバーの更新
		/// </summary>
		private void UpdateReloadBar() {
			if (equipment) {
				reloadBar.fillAmount = equipment.IntervalRatio;
			}
		}

		#endregion
	}
}