using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using EditorUtility;

namespace STG.BaseUtility.Attack {

	public class AttackedEvent : UnityEvent<ObjectAttacker2D> { }
	public class DiedEvent : UnityEvent<AttackableObject2D, ObjectAttacker2D> { }

	/// <summary>
	/// 攻撃可能オブジェクト
	/// </summary>
	[AddComponentMenu("STG/BaseUtility/Attack/AttackableObject2D")]
	[RequireComponent(typeof(Collider2D))]
	public class AttackableObject2D : MonoBehaviour {

		[SerializeField, Range(100, 100000)]
		private int hp = 1000;
		public int HP { get { return hp; } set { hp = nowHP = value; } }
		[SerializeField, Disable]
		private int nowHP;
		public int NowHP { get { return nowHP; } set { nowHP = value; } }

		public float hpRatio { get { return (float)nowHP / hp; } }

		[SerializeField]
		private bool dieWithCollisionDisable = true;

		private Collider2D attackableCollider;
		public Collider2D AttackableCollider { get { return attackableCollider; } }
		private bool isDied;
		public bool IsDied { get { return isDied; } }

		//コールバック
		private AttackedEvent onAttacked;   //攻撃された
		public AttackedEvent OnAttacked { get { return onAttacked; } }
		private DiedEvent onDied;       //倒された
		public DiedEvent OnDied { get { return onDied; } }

		private bool isInited = false;

		#region UnityEvent

		private void Awake() {
			Init();
		}

		#endregion

		#region Function

		/// <summary>
		/// 初期化
		/// </summary>
		public void Init() {
			if (isInited) return;
			attackableCollider = GetComponent<Collider2D>();
			onAttacked = new AttackedEvent();
			onDied = new DiedEvent();
			nowHP = hp;
			isInited = true;
		}

		/// <summary>
		/// 攻撃された
		/// </summary>
		public void Attacked(ObjectAttacker2D attacker) {
			if(!isDied) {
				nowHP -= attacker.Damage;
				if(nowHP <= 0) {
					nowHP = 0;
					Die(attacker);
				} else {
					onAttacked.Invoke(attacker);
				}
			}
		}

		/// <summary>
		/// 回復
		/// </summary>
		public void Recover(int recovery) {
			nowHP += recovery;
			if(nowHP > hp) nowHP = hp;
			if(isDied && nowHP > 0) Reanimate();
		}

		/// <summary>
		/// HP0
		/// </summary>
		private void Die(ObjectAttacker2D attacker) {
			isDied = true;
			if (dieWithCollisionDisable) {
				attackableCollider.enabled = false;
			}
			onDied.Invoke(this, attacker);
		}

		/// <summary>
		/// HP0から復活
		/// </summary>
		private void Reanimate() {
			isDied = false;
			attackableCollider.enabled = true;
		}

		#endregion
	}
}