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
		private STGEquipmentDataObj _equipmentDataObj;

		protected bool _isBusy;	//起動状態
		public bool isBusy { get { return _isBusy; } }

		#region VirtualFunction

		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			StandDownEquipment();
		}

		/// <summary>
		/// 起動状態へ
		/// </summary>
		public virtual void StandUpEquipment() {
			_isBusy = true;
		}

		/// <summary>
		/// 待機状態へ
		/// </summary>
		public virtual void StandDownEquipment() {
			_isBusy = false;
		}

		#endregion
	}
}