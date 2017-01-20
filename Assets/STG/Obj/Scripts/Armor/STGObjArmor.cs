using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingUtility.ComSystem;
using ShootingShip.Attacker;

namespace STG.Obj.Armor {

	/// <summary>
	/// STGオブジェクト用の装甲
	/// </summary>
	public class STGObjArmor : STGCom {

		[SerializeField]
		private AttackableObject2D armor;
		public AttackableObject2D Armor { get { return armor; } }
	}
}