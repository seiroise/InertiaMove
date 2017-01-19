using UnityEngine;
using System;
using ShootingUtility.ComSystem;
using STG.Obj.Detector;

namespace STG.Obj.Targeting {

	/// <summary>
	/// STGオブジェクト用の目標解決器
	/// </summary>
	public class STGObjTargetingResolver : STGCom {

		private STGObjDetector detector;

		#region VirtualFunction

		/// <summary>
		/// 起動
		/// </summary>
		public override void STGAwake() {
			base.STGAwake();
			detector = manager.GetCom<STGObjDetector>();
		}

		#endregion
	}
}