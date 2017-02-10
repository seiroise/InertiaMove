using UnityEngine;
using System;
using System.Collections.Generic;
using STG.Obj;
using STG.BaseUtility.ComSystem;
using STG.Obj.Weapon;
using STG.Bullet.Com;
using STG.BaseUtility.ObjectPool;
using STG.BaseUtility.Attack;

namespace STG.Bullet {

	/// <summary>
	/// STGバレット
	/// </summary>
	public class STGBullet : MonoBehaviour , IPoolable {

		[Header("攻撃")]
		[SerializeField]
		private ObjectAttacker2D attacker;
		public ObjectAttacker2D Attacker { get { return attacker; } }

		[Header("コンポーネント")]
		[SerializeField]
		private STGBulletMove moveCom;
		[SerializeField]
		private STGBulletCom[] coms;

		public float speed {
			set {
				if (moveCom) {
					moveCom.Speed = value;
				}
			}
		}

		private float lifeTime;
		public float LifeTime { get { return lifeTime; } set { lifeTime = value; } }
		private float tLifeTime;
		private float timeRatio;

		private bool isLived;

		#region UnityEvent

		private void Update() {
			if (isLived) {
				UpdateTimer();
			}
		}

		#endregion

		#region Function

		/// <summary>
		/// タイマーの更新
		/// </summary>
		private void UpdateTimer() {
			tLifeTime += Time.deltaTime;
			if (tLifeTime > lifeTime) {
				//削除
				timeRatio = 1f;
				DestroyBullet();
			} else {
				//通常更新
				timeRatio = tLifeTime / lifeTime;
				UpdateComs();
			}
		}

		/// <summary>
		/// 弾の破棄
		/// </summary>
		public void DestroyBullet() {
			DestroyComs();
			gameObject.SetActive(false);
			isLived = false;
		}

		/// <summary>
		/// 弾の初期化(InitPoolableの後に呼ぶこと)
		/// </summary>
		public void InitBullet(STGObjWeapon weapon) {
			InitComs(weapon);
			AwakeComs();
		}

		#endregion

		#region ComFunction

		/// <summary>
		/// 部品の初期化
		/// </summary>
		private void InitComs(STGObjWeapon weapon) {
			moveCom.InitCom(this, weapon);
			foreach (var c in coms) {
				c.InitCom(this, weapon);
			}
		}

		/// <summary>
		/// 部品の起動
		/// </summary>
		private void AwakeComs() {
			moveCom.AwakeCom();
			foreach (var c in coms) {
				c.AwakeCom();
			}
		}

		/// <summary>
		/// 部品の更新
		/// </summary>
		private void UpdateComs() {
			moveCom.UpdateCom();
			foreach (var c in coms) {
				c.UpdateCom();
			}
		}

		/// <summary>
		/// 部品の破棄
		/// </summary>
		private void DestroyComs() {
			foreach (var c in coms) {
				c.DestroyCom();
			}
		}

		#endregion

		#region IPoolable

		/// <summary>
		/// 初期化
		/// </summary>
		public void InitPoolable() {
			tLifeTime = 0f;
			timeRatio = 0f;
			isLived = true;
			if (attacker) {
				attacker.OnAttack.RemoveListener(OnAttack);
				attacker.OnAttack.AddListener(OnAttack);
			}
		}

		#endregion

		#region Callback

		/// <summary>
		/// 攻撃
		/// </summary>
		private void OnAttack(AttackableObject2D attacked) {
			DestroyBullet();
		}

		#endregion
	}
}