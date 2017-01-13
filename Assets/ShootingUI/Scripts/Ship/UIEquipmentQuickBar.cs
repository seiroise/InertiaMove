using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Equipment;
using ShootingShip.Structure;

namespace ShootingUI.Ship {

	/// <summary>
	/// 機体の装備のクイックバー
	/// </summary>
	public class UIEquipmentQuickBar<Equipment, Indicator, Controller, Holder> : MonoBehaviour
		where Equipment : ShipEquipment
		where Indicator : UIEquipmentIndicator<Equipment>
		where Controller : ShipComManager<Holder>
		where Holder : ShipEquipmentHolder<Equipment> {

		[SerializeField]
		protected Indicator[] indicators;

		#region UnityEvent

		private void Awake() {
			Initialize();
		}

		#endregion

		#region Function

		/// <summary>
		/// 初期化
		/// </summary>
		private void Initialize() {
			foreach (var indicator in indicators) {
				indicator.LockEquipment();
			}
		}

		/// <summary>
		/// 装備の一括設定
		/// </summary>
		public void SetEquipments(Controller controller) {
			var holders = controller.Coms;
			for (int i = 0; i < indicators.Length; ++i) {
				if (i < holders.Length) {
					var e = holders[i].Equipment;
					if (e) {
						//装備してる
						indicators[i].SetEquipment(e);
					} else {
						//装備してない
						indicators[i].RemoveEquipment();
					}
				} else {
					//装備できない
					indicators[i].LockEquipment();
				}
			}
		}

		/// <summary>
		/// 装備の設定
		/// </summary>
		public void SetEquipment(int index, Equipment equipment) {
			if (0 <= index && index < indicators.Length) {
				if (indicators[index]) {
					indicators[index].SetEquipment(equipment);
				}
			}
		}

		/// <summary>
		/// 装備の解除
		/// </summary>
		public void RemoveEquipment(int index) {
			if (0 <= index && index < indicators.Length) {
				if (indicators[index]) {
					indicators[index].RemoveEquipment();
				}
			}
		}

		#endregion
	}
}
