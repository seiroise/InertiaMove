using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Structure;
using ShootingShip.Equipment;
using ShootingShip.Bullet;
using ShootingShip.DataObject;
using ShootingShip.Manager;

namespace ShootingShip.Factory {

	/// <summary>
	/// 機体の工場
	/// </summary>
	public class ShipFactory : MonoBehaviour {

		[Header("構造セット")]
		[SerializeField]
		private StructureListDataObject structureList;

		[Header("装備セット")]
		[SerializeField]
		private WeaponListDataObject weaponList;
		[SerializeField]
		private ThrusterListDataObject thrusterList;

		private BulletPoolDictionary bulletPool;

		#region UnityEvent

		private void Start() {
			bulletPool = StageManager.Instance.BulletPool;
		}

		#endregion

		#region Function

		/// <summary>
		/// ランダムな構造の機体を作成
		/// </summary>
		public ShipStructure CreateRandom(string tag) {
			ShipStructure structure = CreateShipStructure(tag);
			SetRandomWeapon(structure);
			SetRandomThruster(structure);
			return structure;
		}

		/// <summary>
		/// 機体構造の作成
		/// </summary>
		public ShipStructure CreateShipStructure(string tag) {
			if (structureList) {
				ShipStructure structure = Instantiate<ShipStructure>(structureList.Get());
				structure.tag = tag;
				structure.InitCom(structure);
				structure.AwakeCom();
				return structure;
			} else {
				return null;
			}
		}

		/// <summary>
		/// ランダムに武器を設定する
		/// </summary>
		public void SetRandomWeapon(ShipStructure structure) {
			var weaponCon = structure.WeaponController;
			foreach (var w in weaponCon.Coms) {
				ShipWeapon weapon = weaponList.Get();
				w.SetEquipment(Instantiate(weapon));
				SetBullet(w.Equipment);
			}
		}

		/// <summary>
		/// 武器に弾プールを設定する
		/// </summary>
		public void SetBullet(ShipWeapon weapon) {
			weapon.BulletPool = bulletPool.RegistObject(weapon.Bullet);
		}

		/// <summary>
		/// ランダムに推進器を設定する
		/// </summary>
		public void SetRandomThruster(ShipStructure structure) {
			var thrusterCon = structure.ThrusterController;
			foreach (var t in thrusterCon.Coms) {
				ShipThruster thruster = thrusterList.Get();
				t.SetEquipment(Instantiate(thruster));
			}
		}

		#endregion
	}
}