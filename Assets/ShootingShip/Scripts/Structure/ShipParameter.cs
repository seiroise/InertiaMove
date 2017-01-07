using UnityEngine;
using System;
using System.Collections.Generic;

namespace ShootingShip.Structure {

	/// <summary>
	/// 機体の装甲
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public class ShipArmor : ShipCom {

		[Header("装甲")]
		[SerializeField, Range(10, 10000)]
		private int armor = 1000;
		public int Armor { get { return armor; } set { armor = value; } }

		#region UnityEvent

		private void OnTriggerEnter2D(Collider2D co) {
			
		}

		#endregion

		#region Function



		#endregion

	}
}