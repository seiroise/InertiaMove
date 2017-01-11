using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Equipment;

namespace ShootingShip.Bullet {

	/// <summary>
	/// 等速直線移動させる弾用部品
	/// </summary>
	public class StraightBulletCom : BulletCom {

		[SerializeField, Range(0.01f, 100f)]
		private float speed = 10f;

		#region VirtualFunction

		public override void InitCom(ShootingBullet owner, ShipWeapon weapon) {
			base.InitCom(owner, weapon);
			speed = weapon.BaseSpeed;
		}

		/// <summary>
		/// 更新
		/// </summary>
		public override void UpdateCom() {
			transform.Translate(new Vector3(speed, 0f, 0f) * Time.deltaTime);
		}

		#endregion
	}
}