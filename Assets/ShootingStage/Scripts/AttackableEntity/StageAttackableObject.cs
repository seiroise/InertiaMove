using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingStage.Entity;
using ShootingShip.Attacker;
using ShootingUtility.ValueIndicator;

namespace ShootingStage.AttackableEntity {

	/// <summary>
	/// ステージ上の攻撃可能オブジェクト
	/// </summary>
	[RequireComponent(typeof(AttackableObject2D))]
	public abstract class StageAttackableObject : StageEntity {

		protected AttackableObject2D attackable;

		#region IPoolable

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitPoolable() {
			base.InitPoolable();
			attackable = GetComponent<AttackableObject2D>();
		}

		#endregion
	}
}