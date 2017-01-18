﻿using UnityEngine;
using System;
using ShootingUtility.ComSystem;
using STG.Obj.DataObj;

namespace STG.Obj.Equipment {

	/// <summary>
	/// STGオブジェクト用の装備
	/// </summary>
	public class STGObjEquipment : STGCom {

		[SerializeField]
		private STGEquipmentDataObj equipmentDataObj;

		private bool isAwaked;	//起動/待機状態

		#region VirtualFunction

		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			StandbyEquipment();
		}

		/// <summary>
		/// 起動状態へ
		/// </summary>
		public virtual void AwakeEquipment() {
			isAwaked = true;
		}

		/// <summary>
		/// 待機状態へ
		/// </summary>
		public virtual void StandbyEquipment() {
			isAwaked = false;
		}

		#endregion
	}
}