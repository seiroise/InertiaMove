using UnityEngine;
using System;
using STG.Obj.Equipment;
using STG.BaseUtility.ObjectDetector;
using STG.BaseUtility.ComSystem;
using STG.Bullet;

namespace STG.Obj.Weapon {

	/// <summary>
	/// STG用の武器
	/// </summary>
	public class STGObjWeapon : STGObjEquipment {

		[Header("パラメータ")]
		[SerializeField]
		private STGObjWeaponParameter baseParameter;
		public STGObjWeaponParameter BaseParameter { get { return baseParameter; } }
		private float bLifeTime;
		[SerializeField]
		private ObjectAttribute targetAttribute;
		public ObjectAttribute TargetAttribute { get { return targetAttribute; } set { targetAttribute = value; } }

		[Header("弾")]
		[SerializeField]
		private STGBullet bullet;
		public STGBullet Bullet { get { return bullet; } }
		private STGBulletPool.Pool bulletPool;
		[SerializeField]
		private Transform[] shotPositions;
		[SerializeField]
		private string bulletExclusionTag = "None";
		public string BulletExclusionTag { get { return bulletExclusionTag; } set { bulletExclusionTag = value; } }

		[Header("コンポーネント")]
		[SerializeField]
		private STGObjWeaponCom[] coms;

		private float tInterval;
		public float intervalRatio { get { return tInterval / baseParameter.Interval; } }

		#region UnityEvent

		private void Update() {
			if (_isBusy) {
				Reload();
			}
		}

		#endregion

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			tInterval = 0f;
			if(baseParameter != null) {
				bLifeTime = baseParameter.lifeTime;
			}
			InitComs();
		}

		/// <summary>
		/// 武器起動
		/// </summary>
		public override void StandUpEquipment() {
			base.StandUpEquipment();
			AwakeComs();
		}

		/// <summary>
		/// 武器待機
		/// </summary>
		public override void StandDownEquipment() {
			base.StandDownEquipment();
			StandbyComs();
		}

		#endregion

		#region Function

		/// <summary>
		/// 再装填
		/// </summary>
		private void Reload() {
			tInterval += Time.deltaTime;
			if (tInterval > baseParameter.Interval) {
				Shot();
				tInterval = 0f;
			}
		}

		/// <summary>
		/// 発射
		/// </summary>
		private void Shot() {
			if (bulletPool != null) {
				foreach (var shotPos in shotPositions) {
					STGBullet b = bulletPool.GetObject(shotPos.position);
					b.transform.eulerAngles = shotPos.eulerAngles;
					b.InitBullet(this);
					b.Attacker.ExclusionTag = bulletExclusionTag;
					b.Attacker.Damage = baseParameter.Damage;
					b.speed = baseParameter.ShotSpeed;
					b.LifeTime = bLifeTime;
					ShotComs(b);
				}
			}
		}
		
		/// <summary>
		/// 弾の設定
		/// </summary>
		public void SetBullet(STGBulletPool pool) {
			if (bulletPool == null) {
				bulletPool = pool.RegistObject(bullet);
			}
		}

		#endregion

		#region ComFunction

		/// <summary>
		/// 初期化
		/// </summary>
		private void InitComs() {
			foreach (var c in coms) {
				c.InitCom(this);
			}
		}

		/// <summary>
		/// 起動
		/// </summary>
		private void AwakeComs() {
			foreach (var c in coms) {
				c.AwakeCom();
			}
		}

		/// <summary>
		/// 待機
		/// </summary>
		private void StandbyComs() {
			foreach (var c in coms) {
				c.StandbyCom();
			}
		}

		/// <summary>
		/// 発射
		/// </summary>
		private void ShotComs(STGBullet bullet) {
			foreach (var c in coms) {
				c.ShotCom(bullet);
			}
		}

		#endregion
	}
}