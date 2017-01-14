using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Structure;
using ShootingShip.DataObject;
using ShootingUI.Ship;

namespace ShootingShip.Equipment {

	/// <summary>
	/// 機体装備
	/// </summary>
	public abstract class ShipEquipment : ShipCom {

		[SerializeField]
		private EquipmentDataObject equipmentData;
		public EquipmentDataObject EquipmentData { get { return equipmentData; } }

		#region VirtualFunction

		/// <summary>
		/// 表示の更新
		/// </summary>
		public virtual void UpdateIndicator(UIEquipmentIndicator indicator) { }

		#endregion
	}
}