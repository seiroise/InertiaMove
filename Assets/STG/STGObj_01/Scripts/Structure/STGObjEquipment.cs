using UnityEngine;
using System;
using ShootingUtility.ComSystem;
using STGObj_01.DataObj;

namespace STGObj_01.Structure {

	/// <summary>
	/// STGオブジェクト用の装備
	/// </summary>
	public class STGObjEquipment : STGCom {

		[SerializeField]
		private STGEquipmentDataObj equipmentDataObj;
	}
}