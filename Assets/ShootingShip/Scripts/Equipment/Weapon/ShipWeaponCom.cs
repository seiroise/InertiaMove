using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Bullet;

namespace ShootingShip.Equipment {

	/// <summary>
	/// 機体武器用の部品
	/// </summary>
	public abstract class ShipWeaponCom : MonoBehaviour {

		protected ShipWeapon weapon;

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public virtual void InitCom(ShipWeapon weapon) {
			this.weapon = weapon;
		}

		/// <summary>
		/// 起動
		/// </summary>
		public virtual void AwakeCom() {
			
		}

		/// <summary>
		/// 待機
		/// </summary>
		public virtual void StandbyCom() {
		
		}

		/// <summary>
		/// 発射
		/// </summary>
		public virtual void FireCom(ShootingBullet bullet) {
			
		}

		#endregion
	}
}