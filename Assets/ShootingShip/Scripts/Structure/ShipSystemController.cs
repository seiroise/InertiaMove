using UnityEngine;
using System;

namespace ShootingShip.Structure {

	/// <summary>
	/// 機体のシステムの操作(管理)
	/// </summary>
	public class ShipSystemController : ShipComManager<ShipSystemHolder> {

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(ShipStructure structure) {
			base.InitCom(structure);
			structure.SystemController = this;
		}

		#endregion
	}
}