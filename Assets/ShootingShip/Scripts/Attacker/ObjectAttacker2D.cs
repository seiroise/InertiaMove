using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace ShootingShip.Attacker {

	public class AttackEvent : UnityEvent<AttackableObject2D> { }

	/// <summary>
	/// オブジェクト攻撃器
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public class ObjectAttacker2D : MonoBehaviour {

		[SerializeField]
		private string exclusionTag;
		public string ExclusionTag { get { return exclusionTag; } set { exclusionTag = value; } }

		[SerializeField, Range(1, 1000)]
		private int damage = 10;
		public int Damage { get { return damage; } set { damage = value; } }

		//コールバック
		private AttackEvent onAttack;   //攻撃した
		public AttackEvent OnAttack { get { return onAttack; } }

		#region UnityEvent

		private void Awake() {
			onAttack = new AttackEvent();
		}

		private void OnTriggerEnter2D(Collider2D co) {
			if(exclusionTag.Equals(co.tag)) return;
			var attacked = co.gameObject.GetComponent<AttackableObject2D>();
			if(attacked) {
				attacked.Attacked(this);
				onAttack.Invoke(attacked);
			}
		}

		#endregion
	}
}