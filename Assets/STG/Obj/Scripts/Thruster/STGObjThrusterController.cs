using UnityEngine;
using System;
using STG.Obj.Equipment;

namespace STG.Obj.Thruster {

	/// <summary>
	/// STGオブジェクト用のスラスタコントローラ
	/// </summary>
	[AddComponentMenu("STG/Obj/Thruster/STGObjThrusterController")]
	public class STGObjThrusterController : STGObjEquipmentController<STGObjThrusterSlot, STGObjThruster> {

		[SerializeField]
		private Rigidbody2D thrustRigidbody2d;
		public Rigidbody2D ThrustRigidbody2D { get { return thrustRigidbody2d; } set { thrustRigidbody2d = value; } }

		#region VirtualFunction

		/// <summary>
		/// 装備の設定
		/// </summary>
		public override STGObjThruster SetEquipment(STGObjThruster equipment, bool isInstantiated) {
			var e = base.SetEquipment(equipment, isInstantiated);
			if(e && thrustRigidbody2d) {
				e.ThrustRigidbody2D = thrustRigidbody2d;
			}
			return e;
		}

		#endregion

		#region Function

		/// <summary>
		/// 推進させる剛体の設定
		/// </summary>
		public void SetThrustRigidbody2D(Rigidbody2D value) {
			EquipmentIterator((e) => {
				if(e) e.ThrustRigidbody2D = value;
			});
		}

		/// <summary>
		/// 燃焼効率の設定
		/// </summary>
		public void SetCombustEfficiency(float value) {
			EquipmentIterator((e) => {
				if(e) {
					e.CombustEfficiency = value;
				}
			});
		}

		#endregion
	}
}