using UnityEngine;
using System;
using System.Collections.Generic;
using STG.Bullet;
using STG.Utility.MassLine;

namespace STG.Bullet.Com {

	/// <summary>
	/// 軌跡の描画
	/// </summary>
	public class STGBulletTrail : STGBulletCom {

		[SerializeField]
		private Gradient gradient;
		[SerializeField, Range(0.01f, 60f)]
		private float lifeTime = 2f;
		[SerializeField]
		private float distanceRatio = 1f;

		private MassLineFactory lineFactory;
		private Line line;
		private TrailUpdater trail;

		private bool isFirstUpdate;

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(STGBullet bullet, Obj.Weapon.STGObjWeapon weapon) {
			base.InitCom(bullet, weapon);
			lineFactory = MassLineFactory.Instance;
		}

		/// <summary>
		/// 起動
		/// </summary>
		public override void AwakeCom() {
			isFirstUpdate = false;
		}

		/// <summary>
		/// 更新
		/// </summary>
		public override void UpdateCom() {
			base.UpdateCom();
			if (!isFirstUpdate) {
				trail = new TrailUpdater(transform, distanceRatio);
				line = lineFactory.CreateLine(Color.white, lifeTime,
					trail,
					new GradientUpdater(gradient));
				isFirstUpdate = true;
			}
		}

		/// <summary>
		/// 破棄
		/// </summary>
		public override void DestroyCom() {
			if (line != null) {
				line.IsAutoDead = true;
				trail.Enable = false;
			}
		}

		#endregion
	}
}