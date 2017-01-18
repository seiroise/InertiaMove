using UnityEngine;
using System;
using STG.Obj.Equipment;

namespace STG.Obj.Weapon {

	/// <summary>
	/// STG用の武器
	/// </summary>
	public class STGObjWeapon : STGObjEquipment {

		[SerializeField]
		private STGObjWeaponParameter baseParameter;

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