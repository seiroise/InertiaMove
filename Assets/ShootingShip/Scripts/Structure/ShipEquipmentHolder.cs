using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Equipment;

namespace ShootingShip.Structure {

	/// <summary>
	/// 装備用ホルダー
	/// </summary>
	public abstract class ShipEquipmentHolder<T> : ShipFacility where T : ShipEquipment {

		[Header("装備")]
		[SerializeField]
		private T equipment;
		public T Equipment { get { return equipment; } }

		private bool inited;

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(ShipStructure structure) {
			base.InitCom(structure);
			if (equipment) SetEquipment(equipment);
			inited = true;
		}

		/// <summary>
		/// 起動
		/// </summary>
		public override void AwakeCom() {
			base.AwakeCom();
			if (equipment) equipment.AwakeCom();
		}

		#endregion

		#region Function

		/// <summary>
		/// 装備の設定
		/// </summary>
		public void SetEquipment(T equipment) {
			if (this.equipment) {
				//取り敢えず削除
				Destroy(equipment.gameObject);
			}
			this.equipment = equipment;
			this.equipment.transform.SetParent(transform, false);
			this.equipment.InitCom(structure);
			if (inited) this.equipment.AwakeCom();
		}

		#endregion
	}
}