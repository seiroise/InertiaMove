using UnityEngine;
using System.Collections;
using STG.Obj.DataObj;
using STG.Obj;
using STG.Obj.Weapon;
using STG.Obj.Thruster;

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

	#region UnityEvent

	private void OnGUI() {
		if (GUILayout.Button("SetWeapon")) {
			if (weaponList) {
				var wCon = targetObj.GetCom<STGObjWeaponController>();
				Debug.Log(wCon);
				if (wCon) {
					wCon.SetEquipment(weaponList.Get(), false);
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