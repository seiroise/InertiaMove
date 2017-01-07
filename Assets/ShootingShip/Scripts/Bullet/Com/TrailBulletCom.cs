using UnityEngine;
using System;
using System.Collections.Generic;
using MassLine;
using ShootingShip.Manager;

namespace ShootingShip.Bullet {

	/// <summary>
	/// 軌跡を描画する弾コン
	/// </summary>
	public class TrailBulletCom : BulletCom {

		[SerializeField]
		private Gradient gradient;
		[SerializeField, Range(0.01f, 60f)]
		private float lifeTime = 2f;
		[SerializeField, Range(0.01f, 10f)]
		private float distanceRatio = 1f;

		private LineEffect line;
		private TrailUpdater trail;

		private bool isFirstUpdate;

		#region VirtualFunction

		/// <summary>
		/// 起動
		/// </summary>
		public override void AwakeCom() {
			isFirstUpdate = false;
		}

		public override void UpdateCom() {
			base.UpdateCom();
			if (!isFirstUpdate) {
				trail = new TrailUpdater(transform, distanceRatio);
				line = StageManager.Instance.LineFactory.CreateLine(
					Color.white, lifeTime,
					trail,
					new GradientUpdater(gradient)
					);
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