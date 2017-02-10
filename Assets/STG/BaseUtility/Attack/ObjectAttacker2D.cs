using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace STG.BaseUtility.Attack {

	public class AttackEvent : UnityEvent<AttackableObject2D> { }

	/// <summary>
	/// オブジェクト攻撃器
	/// </summary>
	[AddComponentMenu("STG/BaseUtility/Attack/ObjectAttacker2D")]
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

		private bool isInited = false;

		#region UnityEvent

		private void Awake() {
			Init();
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

		#region Function

		/// <summary>
		/// 初期化
		/// </summary>
		public void Init() {
			if (isInited) return;
			onAttack = new AttackEvent();
			isInited = true;
		}

		#endregion
	}
}