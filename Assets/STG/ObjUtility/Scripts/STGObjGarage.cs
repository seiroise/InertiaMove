using UnityEngine;
using System;
using System.Collections.Generic;
using STG.Obj.Equipment;
using STG.Obj.DataObj;
using STG.Bullet;
using STG.Obj;
using STG.Obj.Weapon;
using STG.Obj.Thruster;
using STG.Obj.Addon;

namespace STG.ObjUtility {

	/// <summary>
	/// STGObjへのEquipmentの設定
	/// </summary>
	public class STGObjGarage : MonoBehaviour {

		[Header("Bullet")]
		[SerializeField]
		private STGBulletPool _bulletPool;

		#region Function

		/// <summary>
		/// STGObjに指定した武器を装備させる
		/// </summary>
		public void EquipedWeapon(STGObj obj, STGObjWeapon weapon) {
			if(_bulletPool) return;
			//ウエポンコントローラの取得
			var wCon = obj.GetCom<STGObjWeaponController>();
			if (wCon) {
				var w = wCon.SetEquipment(weapon, false);
				//バレットの設定
				if (weapon) {
					weapon.SetBullet(_bulletPool);
				}
			}
		}

		/// <summary>
		/// STGObjに指定した推進器を装備させる
		/// </summary>
		public void EquipedThruster(STGObj obj, STGObjThruster thruster) {
			//スラスタコントローラの取得
			var tCon = obj.GetCom<STGObjThrusterController>();
			if (tCon) {
				var t = tCon.SetEquipment(thruster, false);
			}
		}

		/// <summary>
		/// STGObjに指定したアドオンを装備させる
		/// </summary>
		public void EquipedAddon(STGObj obj, STGObjAddon addon) {
			//アドオンコントローラの取得
			var aCon = obj.GetCom<STGObjAddonController>();
			if (aCon) {
				var a = aCon.SetEquipment(addon, false);
			}
		}

		#endregion
	}
}