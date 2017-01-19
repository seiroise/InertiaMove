using UnityEngine;
using System;
using ShootingUtility.ComSystem;

namespace STG.Obj.Equipment {

	/// <summary>
	/// STGオブジェクト用の装備スロット
	/// </summary>
	public class STGObjEquipmentSlot<E> : STGCom where E : STGObjEquipment {

		[SerializeField]
		private E equipment;
		public E Equipment { get { return equipment; } }

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
		public E SetEquipment(E prefab, bool isInstantiated) {
			isSeted = true;
			//Instantiateされてない場合はInstantiateする
			if(!isInstantiated) {
				prefab = Instantiate(prefab);
			}
			this.equipment = prefab;
			//親子関係の設定
			prefab.transform.SetParent(transform);
			prefab.transform.localPosition = Vector3.zero;
			//初期化
			prefab.STGInit(manager);
			prefab.STGAwake();

			return prefab;
		}

		/// <summary>
		/// 設定されている装備を外す。
		/// </summary>
		public E RemoveEquipment() {
			if(!equipment) return null;
			isSeted = false;
			//親子関係の解除
			equipment.transform.parent = null;
			return equipment;
		}

		#endregion
	}
}