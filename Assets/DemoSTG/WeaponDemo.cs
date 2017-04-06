using UnityEngine;
using System.Collections;
using STG.Bullet;
using STG.Obj.Weapon;
using STG.Obj;

public class WeaponDemo : MonoBehaviour {

	public STGObj stgShip;

	public STGBulletPool bulletPool;
	public STGObjWeapon setWeapon;

	// Use this for initialization
	void Start () {
		SetWeapon();
		SetWeapon();

		stgShip.GetCom<STGObjWeaponController>().AwakeEquipments();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetWeapon() {
		var weaponCon = stgShip.GetCom<STGObjWeaponController>();
		var weapon = weaponCon.SetEquipment(setWeapon, false);
		weapon.SetBullet(bulletPool);
	}
}
