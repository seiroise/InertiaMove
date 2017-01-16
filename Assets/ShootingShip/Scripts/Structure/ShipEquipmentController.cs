using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Equipment;

namespace ShootingShip.Structure {

	/// <summary>
	/// 機体の装備の操作
	/// 型パラメータはInspectorに表示したいので仕方なく２つに
	/// </summary>
	public abstract class ShipEquipmentController<Equipment, Holder> : ShipComManager<Holder>
		where Equipment : ShipEquipment
		where Holder : ShipEquipmentHolder<Equipment> {
	
#region Function

		/// <summary>
		/// 空きホルダーの確認を行う
		/// 空きがある場合はtrueを返す
		/// </summary>
		public bool CheckEmptyHolder() {
			foreach (var c in coms) {
				if (!c.IsSeted) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 空きホルダーに装備を設定する
		/// 空きがなかった場合はfalseを返す
		/// </summary>
		public bool SetEquipment(Equipment equipment) {
			bool set = false;
			foreach (var c in coms) {
				if (!c.IsSeted) {
					c.SetEquipment(equipment);
					set = true;
					break;
				}
			}
			return set;
		}

#endregion
	}
}