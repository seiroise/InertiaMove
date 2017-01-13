using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Structure;
using ShootingShip.Utility;
using ShootingShip.Bullet;

namespace ShootingShip.Equipment {

	/// <summary>
	/// 機体の武器
	/// </summary>
	public class ShipWeapon : ShipEquipment {

		[Header("パラメータ")]
		[SerializeField, Range(1, 2000)]
		private int baseDamage = 100;
		public int BaseDamager { get { return baseDamage; } }
		[SerializeField, Range(0.01f, 20f)]
		private float baseInterval = 1f;
		public float BaseInterval { get { return baseInterval; } }
		[SerializeField, Range(1f, 1000f)]
		private float baseShotRange = 100f;
		public float BaseShotRange { get { return baseShotRange; } }
		[SerializeField, Range(1f, 100f)]
		private float baseSpeed = 10f;
		public float BaseSpeed { get { return baseSpeed; } }

		[Header("弾")]
		[SerializeField]
		private ShootingBullet bullet;
		public ShootingBullet Bullet { get { return bullet; } }
		private BulletPoolDictionary.Pool bulletPool;
		public BulletPoolDictionary.Pool BulletPool { get { return bulletPool; } set { bulletPool = value; } }
		[SerializeField]
		private Transform shotPos;
		[SerializeField]
		private string bulletExclusionTag = "None";
		public string BulletExclusionTag { get { return bulletExclusionTag; } set { bulletExclusionTag = value; } }

		[Header("追加コンポーネント")]
		[SerializeField]
		private ShipWeaponCom[] coms;

		private float tInterval;
		public float IntervalRatio { get { return tInterval / baseInterval; } }
		private bool isAttacked = false;

		#region UnityEvent

		private void Update() {
			if(isAttacked) {
				Reload();
			}
		}

		#endregion

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(ShipStructure ship) {
			base.InitCom(ship);
			tInterval = 0f;
			InitComs();
		}

		#endregion

		#region Attack

		/// <summary>
		/// 再装填
		/// </summary>
		private void Reload() {
			tInterval += Time.deltaTime;
			if(tInterval > baseInterval) {
				Fire();
				tInterval = 0f;
			}
		}

		/// <summary>
		/// 発射
		/// </summary>
		public void Fire() {
			if(bulletPool == null) return;
			ShootingBullet b = bulletPool.GetObject(shotPos.position);
			b.InitBullet(this);
			b.Attacker.ExclusionTag = bulletExclusionTag;
			b.transform.eulerAngles = shotPos.eulerAngles;
			b.Attacker.Damage = baseDamage;
			FireComs(b);
		}

		/// <summary>
		/// 起動
		/// </summary>
		public void WeaponAwake() {
			isAttacked = true;
			AwakeComs();
		}

		/// <summary>
		/// 待機
		/// </summary>
		public void WeaponStandby() {
			isAttacked = false;
			StandbyComs();
		}

		#endregion

		#region Function

		/// <summary>
		/// 初期化
		/// </summary>
		private void InitComs() {
			foreach(var c in coms) {
				c.InitCom(this);
			}
		}

		/// <summary>
		/// 起動
		/// </summary>
		private void AwakeComs() {
			foreach(var c in coms) {
				c.AwakeCom();
			}
		}

		/// <summary>
		/// 待機
		/// </summary>
		private void StandbyComs() {
			foreach(var c in coms) {
				c.StandbyCom();
			}
		}

		/// <summary>
		/// 発射
		/// </summary>
		private void FireComs(ShootingBullet bullet) {
			foreach(var c in coms) {
				c.FireCom(bullet);
			}
		}

		#endregion
	}
}