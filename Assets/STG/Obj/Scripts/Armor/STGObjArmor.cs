using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Attacker;
using STG.BaseUtility.ComSystem;

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