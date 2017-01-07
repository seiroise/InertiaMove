using UnityEngine;
using System;
using System.Collections.Generic;
using MassLine;
using ShootingShip.Manager;

namespace ShootingShip.Bullet {

	/// <summary>
	/// ヒットエフェクトその1
	/// </summary>
	public class HitEffect1BulletCom : BulletCom {

		[SerializeField, Range(1, 10)]
		private int effectNum = 1;
		[SerializeField]
		private Gradient gradient;

		#region VirtualFunction

		/// <summary>
		/// 破棄
		/// </summary>
		public override void DestroyCom() {
			var line = StageManager.Instance.LineFactory.CreateLine(
				Color.white, 0.2f,
				new NattouTrailUpdater(0.4f, transform.position, transform.rotation * new Vector3(0.8f, 0.8f, 0f) * 10f, transform.rotation * Vector3.left * 30f),
				new GradientUpdater(gradient)
				);
			line.IsAutoDead = true;
		}

		#endregion
	}
}