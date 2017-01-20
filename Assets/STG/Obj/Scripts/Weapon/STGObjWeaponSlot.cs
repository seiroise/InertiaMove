using UnityEngine;
using System;
using STG.Obj.Equipment;
using STG.BaseUtility.Lerp;

namespace STG.Obj.Weapon {

	/// <summary>
	/// STGオブジェクト用のウエポンスロット
	/// </summary>
	[AddComponentMenu("STG/Obj/Weapon/STGObjWeaponSlot")]
	public class STGObjWeaponSlot : STGObjEquipmentSlot<STGObjWeapon> {

		[Header("ターゲッティング関連")]
		[SerializeField]
		private LerpAxis2D axis;
		private Transform targetTrans;

		#region UnityEvent

		private void Update() {
			Targeting();
		}

		#endregion

		#region Function

		/// <summary>
		/// ターゲッティング処理
		/// </summary>
		private void Targeting() {
			if (targetTrans && axis) {
				Vector3 d = targetTrans.position - transform.position;
				axis.SetAngle(Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg);
			}
		}

		/// <summary>
		/// ターゲットの設定
		/// </summary>
		public void SetTarget(Transform targetTrans) {
			this.targetTrans = targetTrans;
			if (targetTrans) {
				axis.IsLocal = false;
			} else {
				//デフォの角度へ
				axis.IsLocal = true;
				axis.SetAngle(0f);
			}
		}

		#endregion
	}
}