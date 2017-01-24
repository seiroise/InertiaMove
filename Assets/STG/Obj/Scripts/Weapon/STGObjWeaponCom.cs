using UnityEngine;
using System;
using System.Collections.Generic;
using STG.Bullet;

namespace STG.Obj.Weapon {

	/// <summary>
	/// STGウエポン用のコンポーネント
	/// </summary>
	public abstract class STGObjWeaponCom : MonoBehaviour {

		protected STGObjWeapon weapon;

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public virtual void InitCom(STGObjWeapon weapon) {
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
		public virtual void ShotCom(STGBullet bullet) {

		}

		#endregion
	}
}