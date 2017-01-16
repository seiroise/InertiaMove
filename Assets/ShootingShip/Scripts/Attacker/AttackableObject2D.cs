using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using EditorUtility;

namespace ShootingShip.Attacker {

	public class AttackedEvent : UnityEvent<ObjectAttacker2D> { }
	public class DiedEvent : UnityEvent<AttackableObject2D, ObjectAttacker2D> { }

	/// <summary>
	/// 攻撃可能オブジェクト
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public class AttackableObject2D : MonoBehaviour {

		[SerializeField, Range(100, 100000)]
		private int hp = 1000;
		public int HP { get { return hp; } set { hp = nowHP = value; } }
		[SerializeField, Disable]
		private int nowHP;
		public int NowHP { get { return nowHP; } set { nowHP = value; } }

		public float hpRatio { get { return (float)nowHP / hp; } }

		private Collider2D attackableCollider;
		public Collider2D AttackableCollider { get { return attackableCollider; } }
		private bool isDied;
		public bool IsDied { get { return isDied; } }

		//コールバック
		private AttackedEvent onAttacked;   //攻撃された
		public AttackedEvent OnAttacked { get { return onAttacked; } }
		private DiedEvent onDied;       //倒された
		public DiedEvent OnDied { get { return onDied; } }

		#region UnityEvent

		private void Awake() {
			attackableCollider = GetComponent<Collider2D>();
			onAttacked = new AttackedEvent();
			onDied = new DiedEvent();
			nowHP = hp;
		}

		#endregion

		#region Function

		/// <summary>
		/// 攻撃された
		/// </summary>
		public void Attacked(ObjectAttacker2D attacker) {
			if(!isDied) {
				nowHP -= attacker.Damage;
				if(nowHP <= 0) {
					nowHP = 0;
					isDied = true;
					onDied.Invoke(this, attacker);
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
			if(isDied && nowHP > 0) isDied = false;
		}

		#endregion
	}
}