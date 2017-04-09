using UnityEngine;
using System;
using STG.Obj.Equipment;

namespace STG.Obj.Weapon {

	/// <summary>
	/// STGオブジェクト用の武器制御盤
	/// </summary>
	[AddComponentMenu("STG/Obj/Weapon/STGObjWeaponController")]
	public class STGObjWeaponController : STGObjEquipmentController<STGObjWeaponSlot, STGObjWeapon> {

		#region Function

		/// <summary>
		/// ターゲットの設定
		/// </summary>
		public void SetTarget(int slotNum, Transform targetTrans) {
			if (slotNum < 0 && _comList.Count <= slotNum) return;
			_comList[slotNum].com.SetTarget(targetTrans);
		}

		#endregion
	}
}