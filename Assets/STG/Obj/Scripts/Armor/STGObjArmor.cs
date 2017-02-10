﻿using UnityEngine;
using System;
using System.Collections.Generic;
using STG.BaseUtility.ComSystem;
using STG.BaseUtility.Attack;

namespace STG.Obj.Armor {

	/// <summary>
	/// STGオブジェクト用の装甲
	/// </summary>
	public class STGObjArmor : STGCom {

		[SerializeField]
		private AttackableObject2D armor;
		public AttackableObject2D Armor { get { return armor; } }

		#region VirtualFunction

		/// <summary>
		/// STGオブジェ初期化
		/// </summary>
		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			armor.Init();
		}

		#endregion
	}
}