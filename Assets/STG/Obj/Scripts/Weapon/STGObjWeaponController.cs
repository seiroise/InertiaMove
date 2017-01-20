using UnityEngine;
using System;
using STG.Obj.Equipment;

namespace STG.Obj.Weapon {

	/// <summary>
	/// STG用オブジェクトのウエポンコントローラ
	/// </summary>
	[AddComponentMenu("STG/Obj/Weapon/STGObjWeaponController")]
	public class STGObjWeaponController : STGObjEquipmentController<STGObjWeaponSlot, STGObjWeapon> {

		#region Function

		/// <summary>
		/// ターゲットの設定
		/// </summary>
		public void SetTarget(int slotNum, Transform targetTrans) {
			if (slotNum < 0 && comList.Count <= slotNum) return;
			comList[slotNum].com.SetTarget(targetTrans);
		}

		#endregion
	}
}