using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Utility;

namespace ShootingShip.Structure {

	/// <summary>
	/// 機体設備
	/// </summary>
	public abstract class ShipFacility : ShipCom {

		[Header("回転軸")]
		[SerializeField]
		protected ShipAxis2D axis;
		public ShipAxis2D Axis { get { return axis; } }

		#region Function

		/// <summary>
		/// 角度の設定
		/// </summary>
		public void SetAngle(float angle) {
			axis.SetAngle(angle);
		}

		#endregion
	}
}