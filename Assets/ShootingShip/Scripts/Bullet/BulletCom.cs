using UnityEngine;
using System;
using System.Collections.Generic;

namespace ShootingShip.Bullet {

	/// <summary>
	/// 弾コンポーネント
	/// </summary>
	[Serializable]
	public abstract class BulletCom : MonoBehaviour {

		protected ShootingBullet owner;

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public virtual void InitCom(ShootingBullet owner) {
			this.owner = owner;
		}

		/// <summary>
		/// 起動
		/// </summary>
		public virtual void AwakeCom() { }

		/// <summary>
		/// 更新
		/// </summary>
		public virtual void UpdateCom() { }

		/// <summary>
		/// 破棄
		/// </summary>
		public virtual void DestroyCom() { }

		#endregion
	}
}