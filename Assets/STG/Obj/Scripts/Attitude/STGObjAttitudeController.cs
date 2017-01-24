using UnityEngine;
using System;
using STG.Obj.Thruster;
using STG.BaseUtility.Lerp;
using STG.BaseUtility.ComSystem;

namespace STG.Obj.Attitude {

	/// <summary>
	/// STGオブジェクト用の姿勢制御器
	/// </summary>
	public class STGObjAttitudeController : STGCom {

		[SerializeField]
		private LerpAxis2D axis;

		private STGObjThrusterController thruster;

		#region VirtualFunction

		public override void STGAwake() {
			base.STGAwake();
			thruster = manager.GetCom<STGObjThrusterController>();
			if(!thruster) {
				Debug.LogError("manager not has " + thruster.GetType());
			}
		}

		#endregion

		#region Function

		/// <summary>
		/// 移動
		/// </summary>
		public void Move(float angle, float combustEfficiency) {
			if(axis) {
				axis.SetAngle(angle);
			}
			if(thruster) {
				thruster.SetCombustEfficiency(combustEfficiency);
			}
		}

		/// <summary>
		/// 移動
		/// </summary>
		public void Move(float combustEfficiency) {
			if(thruster) {
				thruster.SetCombustEfficiency(combustEfficiency);
			}
		}

		/// <summary>
		/// 移動
		/// </summary>
		public void Move(Vector2 direction, float combustEfficiency) {
			Move(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, combustEfficiency);
		}

		#endregion
	}
}