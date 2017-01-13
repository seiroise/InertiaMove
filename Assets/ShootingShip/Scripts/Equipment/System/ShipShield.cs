using UnityEngine;
using System;
using ShootingShip.Attacker;

namespace ShootingShip.Equipment {

	/// <summary>
	/// 機体のシールド
	/// </summary>
	public class ShipShield : ShipSystem {

		[Header("コンポーネント")]
		[SerializeField]
		private AttackableObject2D attackableShield;
		[SerializeField]
		private CircleCollider2D shieldCollider;
		[SerializeField]
		private SpriteRenderer shieldSprite;

		[Header("パラメータ")]
		[SerializeField, Range(0, 10000)]
		private int shieldDurability = 1000;	//耐久力
		[SerializeField, Range(100, 2000)]
		private int shieldRegenerate = 200;		//自動再生(/秒)
		[SerializeField, Range(0f, 120f)]
		private float shieldRecoverTime = 20f;	//再復活時間(秒)

		private AttackableObject2D mainAttackable;	//攻撃されてない時間を測る攻撃可能オブジェクト
		private float attackedTimer = 0f;			//攻撃されてない時間を測るタイマー
		private bool isAwakedShield;				//シールドが展開されてるか

		#region UnityEvent

		private void Update() {
			if(isAwakedShield) {
				RegenerateShield();
			} else {
				RecoverShield();
			}
		}

		#endregion

		#region VirtualFunction

		/// <summary>
		/// 起動
		/// </summary>
		public override void AwakeCom() {
			base.AwakeCom();
			//コールバックの登録
			if(attackableShield) {
				attackableShield.OnAttacked.RemoveListener(OnAttackedShield);
				attackableShield.OnAttacked.AddListener(OnAttackedShield);
				attackableShield.OnDied.RemoveListener(OnDestroyedShield);
				attackableShield.OnDied.AddListener(OnDestroyedShield);
			}
			//シールド起動
			AwakeShield();
		}

		#endregion

		#region Function

		/// <summary>
		/// シールド起動
		/// </summary>
		private void AwakeShield() {
			isAwakedShield = true;
			attackableShield.HP = shieldDurability;
			shieldCollider.enabled = true;
		}

		/// <summary>
		/// シールド破壊
		/// </summary>
		private void DestroyShield() {
			isAwakedShield = false;
			shieldCollider.enabled = false;
			attackedTimer = 0f;
		}

		/// <summary>
		/// シールドの回復(破壊状態から起動状態へ)
		/// </summary>
		private void RecoverShield() {
			attackedTimer += Time.deltaTime;
			if(attackedTimer > shieldRecoverTime) {
				//シールド復活
				AwakeShield();
			}
		}

		/// <summary>
		/// シールドの自己再生
		/// </summary>
		private void RegenerateShield() {
			attackableShield.Recover((int)(shieldRegenerate * Time.deltaTime));
		}

		#endregion

		#region Callback

		private void OnAttackedMain(ObjectAttacker2D attacker) {
			
		}

		/// <summary>
		/// シールドが攻撃された時
		/// </summary>
		private void OnAttackedShield(ObjectAttacker2D attacker) {
			//それっぽいエフェクトを表示
		}

		/// <summary>
		/// シールドが破壊された時
		/// </summary>
		private void OnDestroyedShield(AttackableObject2D attackable, ObjectAttacker2D attacker) {
			DestroyShield();
		}

		#endregion
	}
}