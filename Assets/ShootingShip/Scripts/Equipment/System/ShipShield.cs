using UnityEngine;
using System;
using ShootingShip.Attacker;
using ShootingUtility.Lerp;
using ShootingUI.Ship;

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
		private LerpSpriteAlpha spriteAlpha;

		[Header("パラメータ")]
		[SerializeField, Range(0, 10000)]
		private int shieldDurability = 1000;	//耐久力
		[SerializeField, Range(100, 2000)]
		private int shieldRegenerate = 200;		//自動再生(/秒)
		[SerializeField, Range(0f, 120f)]
		private float shieldRecoverTime = 20f;	//再復活時間(秒)

		private AttackableObject2D shipAttackable;	//攻撃されてない時間を測る攻撃可能オブジェクト
		private float recoverTimer = 0f;			//再展開までの時間を測るタイマー
		public float recoverRatio { get { return recoverTimer / shieldRecoverTime; } }
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
			shipAttackable = structure.Attackable;
			if (shipAttackable) {
				shipAttackable.OnAttacked.RemoveListener(OnAttackedMain);
				shipAttackable.OnAttacked.AddListener(OnAttackedMain);
			}
			//タグの変更
			shieldCollider.tag = shipAttackable.tag;
			//シールド起動
			AwakeShield();
		}

		public override void UpdateIndicator(UIEquipmentIndicator indicator) {
			base.UpdateIndicator(indicator);
			//シールド状態を反映
			if (isAwakedShield) {
				indicator.RatioBar.fillAmount = attackableShield.hpRatio;
			} else {
				indicator.RatioBar.fillAmount = recoverRatio;
			}
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
			//機体のコライダーをオフ
			shipAttackable.AttackableCollider.enabled = false;
			//表示
			ShieldHitEffect();
		}

		/// <summary>
		/// シールド破壊
		/// </summary>
		private void DestroyShield() {
			isAwakedShield = false;
			shieldCollider.enabled = false;
			//機体のコライダーをオン
			shipAttackable.AttackableCollider.enabled = true;
			recoverTimer = 0f;
		}

		/// <summary>
		/// シールドの回復(破壊状態から起動状態へ)
		/// </summary>
		private void RecoverShield() {
			recoverTimer += Time.deltaTime;
			if(recoverTimer > shieldRecoverTime) {
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

		/// <summary>
		/// ヒットエフェクトっぽいもの
		/// </summary>
		private void ShieldHitEffect() {
			if (spriteAlpha) {
				spriteAlpha.ForceSpriteAlpha(1f);
				spriteAlpha.SetSpriteAlpha(0f);
			}
		}

		#endregion

		#region Callback

		/// <summary>
		/// 本体が攻撃された時
		/// </summary>
		private void OnAttackedMain(ObjectAttacker2D attacker) {
			//未展開時なら再展開時間を0に戻す
			if (!isAwakedShield) {
				recoverTimer = 0f;
			}
		}

		/// <summary>
		/// シールドが攻撃された時
		/// </summary>
		private void OnAttackedShield(ObjectAttacker2D attacker) {
			ShieldHitEffect();
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