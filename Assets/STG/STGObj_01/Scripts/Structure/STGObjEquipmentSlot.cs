using UnityEngine;
using System;
using ShootingUtility.ComSystem;

namespace STGObj_01.Structure {

	/// <summary>
	/// STGオブジェクト用の装備スロット
	/// </summary>
	public class STGObjEquipmentSlot : STGCom {

		[SerializeField]
		private STGObjEquipment equipment;

		private bool isSeted;
		public bool IsSeted { get { return isSeted; } }

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			if(equipment) equipment.STGInit(manager);
		}

		/// <summary>
		/// 起動
		/// </summary>
		public override void STGAwake() {
			base.STGAwake();
			if(equipment) equipment.STGAwake();
		}

		#endregion

		#region Function

		/// <summary>
		/// 装備を設定する。
		/// 生成を同時に行う場合はisInstantiatedをfalseにする。
		/// </summary>
		public void SetEquipment(STGObjEquipment equipment, bool isInstantiated) {
			isSeted = true;
			//Instantiateされてない場合はInstantiateする
			if(!isInstantiated) {
				equipment = Instantiate(equipment);
			}
			//親子関係の設定
			equipment.transform.SetParent(transform);
		}

		/// <summary>
		/// 設定されている装備を外す。
		/// </summary>
		public void RemoveEquipment() {
			if(!equipment) return;
			isSeted = false;
			//親子関係の解除
			equipment.transform.parent = null;
		}

		#endregion
	}
}