using UnityEngine;
using UnityEngine.Events;
using System;
using STG.BaseUtility.ComSystem;

namespace STG.Obj.Equipment {

	/// <summary>
	/// STGオブジェクト用の設備操作器
	/// </summary>
	public class STGObjEquipmentController<Slot, Equipment> : STGAbstractComManager<Slot>
		where Slot : STGObjEquipmentSlot<Equipment>
		where Equipment : STGObjEquipment {

		public class EquipmentEvent : UnityEvent<int, Equipment> {}

		//コールバック
		private EquipmentEvent _onSet;
		public EquipmentEvent onSet { get { return _onSet; } }
		private EquipmentEvent _onRemove;
		public EquipmentEvent onRemove { get { return _onRemove; } }

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			_onSet = new EquipmentEvent();
			_onRemove = new EquipmentEvent();
		}

		/// <summary>
		/// 装備を指定したスロットに設定する。
		/// </summary>
		public virtual Equipment SetEquipment(int slot, Equipment equipment, bool isInstantiated) {
			if(slot < 0 || _comList.Count <= slot) return null;
			if(!_comList[slot].com.isSeted) {
				var e = _comList[slot].com.SetEquipment(equipment, isInstantiated);
				if(e) _onSet.Invoke(slot, e);
				return e;
			} else {
				return null;
			}
		}

		/// <summary>
		/// 装備を空きスロットに設定する。
		/// 生成を同時に行う場合はisInstantiatedをfalseにする。
		/// </summary>
		public virtual Equipment SetEquipment(Equipment equipment, bool isInstantiated) {
			int i = 0;
			foreach(var c in _comList) {
				if(!c.com.isSeted) {
					return SetEquipment(i, equipment, isInstantiated);
				}
				++i;
			}
			return null;
		}

		/// <summary>
		/// 指定したスロットに設定されている装備を解除する
		/// </summary>
		public virtual Equipment RemoveEquipment(int slot) {
			if(slot < 0 || _comList.Count <= slot) return null;
			var e = _comList[slot].com.RemoveEquipment();
			if(e) _onRemove.Invoke(slot, e);
			return e;
		}

		#endregion

		#region Function

		/// <summary>
		/// 装備を巡回するためのイテレータ
		/// </summary>
		public void EquipmentIterator(Action<Equipment> action) {
			Equipment e;
			foreach(var c in _comList) {
				e = c.com.equipment;
				if(e) {
					action.Invoke(e);
				}
			}
		}

		/// <summary>
		/// 装備を巡回するためのイテレータ
		/// </summary>
		public void EquipmentIterator(Action<int, Equipment> action) {
			Equipment e;
			int i = 0;
			foreach (var c in _comList) {
				e = c.com.equipment;
				if (e) {
					action.Invoke(i, e);
				}
				i++;
			}
		}

		/// <summary>
		/// 全ての装備を起動状態へ
		/// </summary>
		public void AwakeEquipments() {
			EquipmentIterator((Equipment e) => {
				e.StandUpEquipment();
			});
		}

		/// <summary>
		/// 全ての装備を待機状態へ
		/// </summary>
		public void StandbyEquipments() {
			EquipmentIterator((Equipment e) => {
				e.StandDownEquipment();
			});
		}

		#endregion
	}
}