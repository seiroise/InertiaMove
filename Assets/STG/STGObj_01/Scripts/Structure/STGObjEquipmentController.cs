using System;
using ShootingUtility.ComSystem;

namespace STGObj_01.Structure {

	/// <summary>
	/// STGオブジェクト用の設備操作器
	/// </summary>
	public class STGObjEquipmentController : STGAbstractComManager<STGObjEquipmentSlot> {

		#region Function

		/// <summary>
		/// 装備を空きスロットに設定する。
		/// 生成を同時に行う場合はisInstantiatedをfalseにする。
		/// </summary>
		public void SetEquipment(STGObjEquipment equipment, bool isInstantiated) {
			foreach(var c in comList) {
				if(!c.com.IsSeted) {
					c.com.SetEquipment(equipment, isInstantiated);
					return;
				}
			}
		}

		/// <summary>
		/// 指定したスロットに設定されている装備を解除する
		/// </summary>
		public void RemoveEquipment(int index) {
			if(index < 0 && comList.Count <= index) return;
			comList[index].com.RemoveEquipment();
		}

		#endregion
	}
}