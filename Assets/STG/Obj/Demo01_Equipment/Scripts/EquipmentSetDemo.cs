using UnityEngine;
using System.Collections;
using STG.Obj.DataObj;
using STG.Obj;
using STG.Obj.Weapon;
using STG.Obj.Thruster;
using STG.Bullet;

/// <summary>
/// 装備の設定デモ
/// </summary>
public class EquipmentSetDemo : MonoBehaviour {

	[SerializeField]
	private STGObj targetObj;
	
	[Header("Equipment")]
	[SerializeField]
	private STGWeaponListDataObj weaponList;
	[SerializeField]
	private STGThrusterListDataObj thrusterList;
	[SerializeField]
	private STGAddonListDataObj addonList;

	[Header("Bullet")]
	[SerializeField]
	private STGBulletPool bulletPool;

	#region UnityEvent

	private void OnGUI() {
		if (GUILayout.Button("SetWeapon")) {
			if (weaponList) {
				//ウエポンコントローラの取得
				var wCon = targetObj.GetCom<STGObjWeaponController>();
				Debug.Log(wCon);
				if (wCon) {
					var weapon = wCon.SetEquipment(weaponList.Get(), false);
					//バレットの設定
					if (weapon && bulletPool) {
						weapon.SetBullet(bulletPool);
					}
				}
			}
		}
		if (GUILayout.Button("SetThruster")) {
			if (thrusterList) {
				var tCon = targetObj.GetCom<STGObjThrusterController>();
				Debug.Log(tCon);
				if (tCon) {
					tCon.SetEquipment(thrusterList.Get(), false);
				}
			}
		}
		if (GUILayout.Button("SetAddon")) {

		}
	}

	#endregion
}