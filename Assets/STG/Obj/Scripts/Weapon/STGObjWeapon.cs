using UnityEngine;
using System;
using STG.Obj.Equipment;
using STG.BaseUtility.ObjectDetector;

namespace STG.Obj.Weapon {

	/// <summary>
	/// STG用の武器
	/// </summary>
	public class STGObjWeapon : STGObjEquipment {

		[SerializeField]
		private STGObjWeaponParameter baseParameter;
		public STGObjWeaponParameter BaseParameter { get { return baseParameter; } }
		[SerializeField]
		private ObjectAttribute targetAttribute;
		public ObjectAttribute TargetAttribute { get { return targetAttribute; } set { targetAttribute = value; } }

		#region UnityEvent

		private void Update() {
			
		}

		#endregion

		#region Function

		/// <summary>
		/// 再装填
		/// </summary>
		private void Reload() {
			
		}

		#endregion
	}
}