using UnityEngine;
using System;
using STG.Obj.Equipment;

namespace STG.Obj.DataObj {
	
	public class STGEquipmentDataObj : STGDataObj {

		[SerializeField]
		private STGObjEquipment _prefab;
		public STGObjEquipment prefab { get { return _prefab; } }
		[SerializeField]
		private Sprite _appearance;
		public Sprite appearance { get { return _appearance; } }
	}
}