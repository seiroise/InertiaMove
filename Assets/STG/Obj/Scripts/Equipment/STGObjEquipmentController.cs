using UnityEngine;
using UnityEngine.Events;
using System;
using ShootingUtility.ComSystem;

namespace STG.Obj.Equipment {

	/// <summary>
	/// STGオブジェクト用の設備操作器
	/// </summary>
	public class STGObjEquipmentController<Slot, Equipment> : STGAbstractComManager<Slot>
		where Slot : STGObjEquipmentSlot<Equipment>
		where Equipment : STGObjEquipment {

		public class EquipmentEvent : UnityEvent<int, Equipment> {}

		//コールバック
		private EquipmentEvent onSet;
		public EquipmentEvent OnSet { get { return onSet; } }
		private EquipmentEvent onRemove;
		public EquipmentEvent OnRemove { get { return onRemove; } }

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			onSet = new EquipmentEvent();
			onRemove = new EquipmentEvent();
		}

		/// <summary>
		/// 装備を空きスロットに設定する。
		/// 生成を同時に行う場合はisInstantiatedをfalseにする。
		/// </summary>
		public virtual Equipment SetEquipment(Equipment prefab, bool isInstantiated) {
			int i = 0;
			foreach(var c in comList) {
				if(!c.com.IsSeted) {
					var e = c.com.SetEquipment(prefab, isInstantiated);
					if(e) onSet.Invoke(i, e);
					return e;
				}
				++i;
			}
			return null;
		}

		/// <summary>
		/// 指定したスロットに設定されている装備を解除する
		/// </summary>
		public virtual Equipment RemoveEquipment(int index) {
			if(index < 0 && comList.Count <= index) return null;
			var e = comList[index].com.RemoveEquipment();
			if(e) onRemove.Invoke(index, e);
			return e;
		}

		#endregion

		#region Function

		/// <summary>
		/// 装備を巡回するためのイテレータ
		/// </summary>
		public void EquipmentIterator(Action<Equipment> action) {
			Equipment e;
			foreach(var c in comList) {
				e = c.com.Equipment;
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
			foreach (var c in comList) {
				e = c.com.Equipment;
				if (e) {
					action.Invoke(i, e);
				}
				i++;
			}
		}

		#endregion
	}
}