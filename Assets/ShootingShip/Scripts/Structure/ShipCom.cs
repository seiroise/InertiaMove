using UnityEngine;
using System;
using System.Collections.Generic;
namespace ShootingShip.Structure {

	/// <summary>
	/// 機体の部品
	/// </summary>
	public abstract class ShipCom : MonoBehaviour {

		protected ShipStructure structure;

		#region VirtualFunction

		/// <summary>
		/// 部品の初期化
		/// </summary>
		public virtual void InitCom(ShipStructure structure) {
			this.structure = structure;
		}

		/// <summary>
		/// 部品の起動
		/// </summary>
		public virtual void AwakeCom() { }

		#endregion
	}
}