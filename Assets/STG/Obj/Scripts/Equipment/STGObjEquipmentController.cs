using UnityEngine;
using System;
using ShootingUtility.ComSystem;

namespace STG.Obj.Equipment {

	/// <summary>
	/// STGオブジェクト用の設備操作器
	/// </summary>
	public class STGObjEquipmentController<Slot, Equipment> : STGAbstractComManager<Slot>
		where Slot : STGObjEquipmentSlot<Equipment>
		where Equipment : STGObjEquipment {

		#region VirtualFunction

		/// <summary>
		/// 装備を空きスロットに設定する。
		/// 生成を同時に行う場合はisInstantiatedをfalseにする。
		/// </summary>
		public virtual Equipment SetEquipment(Equipment prefab, bool isInstantiated) {
			foreach(var c in comList) {
				if(!c.com.IsSeted) {
					return c.com.SetEquipment(prefab, isInstantiated);
				}
			}
			return null;
		}

		/// <summary>
		/// 指定したスロットに設定されている装備を解除する
		/// </summary>
		public virtual Equipment RemoveEquipment(int index) {
			if(index < 0 && comList.Count <= index) return null;
			return comList[index].com.RemoveEquipment();
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