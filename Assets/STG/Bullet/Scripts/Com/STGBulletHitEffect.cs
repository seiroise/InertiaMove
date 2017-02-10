using UnityEngine;
using System;
using System.Collections.Generic;
using STG.Utility.MassLine;

namespace STG.Bullet.Com {
	
	/// <summary>
	/// STGバレット用のヒットエフェクトコンポーネント
	/// </summary>
	public class STGBulletHitEffect : STGBulletCom {

		[SerializeField]
		private float lifeTime = 1f;
		[SerializeField, Range(3, 30)]
		private int vertNum = 6;
		[SerializeField, Range(0.01f, 100f)]
		private float speed = 10f;
		[SerializeField]
		private Gradient gradient;

		private MassLineFactory lineFactory;

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(STGBullet bullet, Obj.Weapon.STGObjWeapon weapon) {
			base.InitCom(bullet, weapon);
			lineFactory = MassLineFactory.Instance;
		}

		/// <summary>
		/// 破棄
		/// </summary>
		public override void DestroyCom() {
			base.DestroyCom();
			var line = lineFactory.CreateLine(Color.white, lifeTime,
				new CircleUpdater(transform.position, speed, vertNum),
				new GradientUpdater(gradient));
			line.IsAutoDead = true;
		}

		#endregion
	}
}