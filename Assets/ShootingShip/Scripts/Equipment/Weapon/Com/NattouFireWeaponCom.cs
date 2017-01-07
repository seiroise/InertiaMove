using UnityEngine;
using System;
using System.Collections.Generic;

namespace ShootingShip.Equipment {

	/// <summary>
	/// 発射エフェクト
	/// </summary>
	public class NattouFireWeaponCom : ShipWeaponCom {

		#region VirtualFunction

		public override void FireCom(Bullet.ShootingBullet bullet) {
			base.FireCom(bullet);

		}

		#endregion
	}
}