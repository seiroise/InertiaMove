using UnityEngine;
using System;
using System.Collections.Generic;
using STG.BaseUtility.ComSystem;
using STG.Obj.Weapon;

namespace STG.Bullet {
	
	/// <summary>
	/// STGバレット用のコンポーネント
	/// </summary>
	public abstract class STGBulletCom : MonoBehaviour {

		protected STGBullet bullet;
		protected STGObjWeapon weapon;

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public virtual void InitCom(STGBullet bullet, STGObjWeapon weapon) {
			this.bullet = bullet;
			this.weapon = weapon;
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