using UnityEngine;
using System.Collections;
using ShootingShip.Equipment;
using ShootingShip.Structure;

namespace ShootingUI.Ship {

	/// <summary>
	/// 武器クイックバー
	/// </summary>
	public class UIWeaponQuickBar : UIEquipmentQuickBar<ShipWeapon, UIWeaponIndicator, ShipWeaponController, ShipWeaponHolder> { }
}