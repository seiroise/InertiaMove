using UnityEngine;
using System;
using STG.BaseUtility.ComSystem;

namespace STG.Obj.Equipment {

	/// <summary>
	/// STGオブジェクト用の装備スロット
	/// </summary>
	public class STGObjEquipmentSlot<Equipment> : STGCom where Equipment : STGObjEquipment {

		[SerializeField]
		protected Equipment _equipment;
		public Equipment equipment { get { return _equipment; } }

		private bool _isSeted;
		public bool isSeted { get { return _isSeted; } }

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			if(_equipment) _equipment.STGInit(manager);
		}

		/// <summary>
		/// 起動
		/// </summary>
		public override void STGAwake() {
			base.STGAwake();
			if(_equipment) _equipment.STGAwake();
		}

		#endregion

		#region Function

		/// <summary>
		/// 装備を設定する。
		/// 生成を同時に行う場合はisInstantiatedをfalseにする。
		/// </summary>
		public Equipment SetEquipment(Equipment prefab, bool isInstantiated) {
			_isSeted = true;
			//Instantiateされてない場合はInstantiateする
			if(!isInstantiated) {
				prefab = Instantiate(prefab);
			}
			this._equipment = prefab;
			//親子関係の設定
			prefab.transform.SetParent(transform);
			prefab.transform.localPosition = Vector3.zero;
			prefab.transform.localEulerAngles = Vector3.zero;
			//初期化
			prefab.STGInit(manager);
			prefab.STGAwake();

			return prefab;
		}

		/// <summary>
		/// 設定されている装備を外す。
		/// </summary>
		public Equipment RemoveEquipment() {
			if(!_equipment) return null;
			_isSeted = false;
			//親子関係の解除
			_equipment.transform.parent = null;
			return _equipment;
		}

		#endregion
	}
}