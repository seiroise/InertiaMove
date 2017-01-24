using UnityEngine;
using System;
using STG.Obj.DataObj;
using STG.BaseUtility.ComSystem;

namespace STG.Obj.Equipment {

	/// <summary>
	/// STGオブジェクト用の装備
	/// </summary>
	public class STGObjEquipment : STGCom {

		[SerializeField]
		private STGEquipmentDataObj equipmentDataObj;

		protected bool isAwaked;	//起動/待機状態
		public bool IsAwaked { get { return isAwaked; } }

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