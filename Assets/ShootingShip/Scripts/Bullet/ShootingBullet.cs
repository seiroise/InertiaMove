using UnityEngine;
using System.Collections;
using ShootingUtility.ObjectPool;
using ShootingShip.Attacker;

namespace ShootingShip.Bullet {

	/// <summary>
	/// 武器用の弾
	/// </summary>
	public class ShootingBullet : MonoBehaviour, IPoolable {

		[SerializeField, Range(0.1f, 120f)]
		protected float lifeTime = 5f;
		public float LifeTime { get { return lifeTime; } }
		private float tLifeTime;
		private float timeRatio;    //tLifeTime / lifeTime
		public float TimeRatio { get { return timeRatio; } }
		[SerializeField]
		private bool startWithLive = true;
		private bool isLived;

		[Header("攻撃")]
		[SerializeField]
		private ObjectAttacker2D attacker;
		public ObjectAttacker2D Attacker { get { return attacker; } }

		[Header("耐久")]
		[SerializeField, Range(1, 5)]
		private int armor = 1;

		[Header("部品")]
		[SerializeField]
		private BulletCom[] coms;

		#region UnityEvent

		private void Start() {
			if(startWithLive) {
				InitPoolable();
			}
		}

		private void Update() {
			if(isLived) {
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
			if(tLifeTime > lifeTime) {
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
		/// 部品の初期化
		/// </summary>
		private void InitComs() {
			foreach(var c in coms) {
				c.InitCom(this);
			}
		}

		/// <summary>
		/// 部品の起動
		/// </summary>
		private void AwakeComs() {
			foreach(var c in coms) {
				c.AwakeCom();
			}
		}

		/// <summary>
		/// 部品の更新
		/// </summary>
		private void UpdateComs() {
			foreach(var c in coms) {
				c.UpdateCom();
			}
		}

		/// <summary>
		/// 部品の破棄
		/// </summary>
		private void DestroyComs() {
			foreach(var c in coms) {
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
			isLived = true;
			if(attacker) attacker.OnAttack.AddListener(OnAttack);
			InitComs();
			AwakeComs();
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