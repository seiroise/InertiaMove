using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ShootingShip.Equipment;

namespace ShootingUI.Ship {

	/// <summary>
	/// 機体の装備の表示
	/// </summary>
	public abstract class UIShipEquipmentIndicator<T> : MonoBehaviour where T : ShipEquipment {

		[Header("UI")]
		[SerializeField]
		private Image equipmentImage;
		[SerializeField]
		private Text name;

		#region VirtualFunction

		/// <summary>
		/// 装備の設定
		/// </summary>
		public abstract void SetEquipment(T equipment);

		/// <summary>
		/// 装備の解除
		/// </summary>
		public abstract void RemoveEquipment();

		#endregion
	}
}