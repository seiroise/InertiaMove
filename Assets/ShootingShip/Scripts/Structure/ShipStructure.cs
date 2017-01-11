using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Attacker;

namespace ShootingShip.Structure {

	/// <summary>
	/// 機体構造
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D), typeof(AttackableObject2D))]
	public class ShipStructure : ShipComManager<ShipCom> {

		//コンポーネント群
		private Rigidbody2D rBody2d;
		public Rigidbody2D RBody2d { get { return rBody2d; } }

		private AttackableObject2D attackable;
		public AttackableObject2D Attackable { get { return attackable; } }

		private ShipWeaponController weaponController;
		public ShipWeaponController WeaponController { get { return weaponController; } set { weaponController = value; } }

		private ShipThrusterController thrusterController;
		public ShipThrusterController ThrusterController { get { return thrusterController; } set { thrusterController = value; } }

		private ShipAttitudeController attitudeController;
		public ShipAttitudeController AttitudeController { get { return attitudeController; } set { attitudeController = value; } }

		private ShipMarker marker;
		public ShipMarker Marker { get { return marker; } set { marker = value; } }

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(ShipStructure structure) {
			base.InitCom(structure);
			rBody2d = GetComponent<Rigidbody2D>();
			attackable = GetComponent<AttackableObject2D>();
		}

		#endregion
	}
}