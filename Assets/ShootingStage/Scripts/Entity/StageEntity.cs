using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingStage.Object;
using ShootingUtility.ObjectDetector;

namespace ShootingStage.Entity {

	/// <summary>
	/// ステージ上のエンティティ
	/// </summary>
	[RequireComponent(typeof(DetectableRigidbody2D))]
	public abstract class StageEntity : StageObject {

		protected DetectableRigidbody2D detectable;

		#region IPoolable

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitPoolable() {
			base.InitPoolable();
			detectable = GetComponent<DetectableRigidbody2D>();
		}

		#endregion
	}
}