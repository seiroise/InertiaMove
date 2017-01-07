using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingUtility.Particle;
using ShootingShip.Manager;

namespace ShootingShip.Bullet {

	/// <summary>
	/// パーティクルエフェクト用の弾コン
	/// </summary>
	public class ParticleEffectBulletCom : BulletCom {

		[SerializeField]
		private ObjectPoolParticle destroyEffect;
		private ParticlePoolDictionary.Pool destroyEffectPool;

		#region VirtuaFunction

		public override void InitCom(ShootingBullet owner) {
			base.InitCom(owner);
			if (destroyEffectPool == null) {
				var pool = StageManager.Instance.ParticlePool;
				destroyEffectPool = pool.RegistObject(destroyEffect);
			}
		}

		public override void DestroyCom() {
			base.DestroyCom();
			if (destroyEffectPool != null) {
				var obj = destroyEffectPool.GetObject(transform.position);
				obj.transform.eulerAngles += transform.eulerAngles;
			}
		}

		#endregion
	}
}