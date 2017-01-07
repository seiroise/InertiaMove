using UnityEngine;
using System;
using System.Collections.Generic;

namespace InertiaShooting.Weapon {

	/// <summary>
	/// 武器の管理
	/// </summary>
	[Serializable]
	public class WeaponManager {

		[SerializeField]
		private Rotation2D mainAxis;		//中心回転軸

		[SerializeField]
		private WeaponController[] weapons;	//武器

		#region Function

		/// <summary>
		/// 武器の角度を更新
		/// </summary>
		public void UpdateAngle(Transform target) {
			Vector3 targetPos = target.position;
			Vector3 d;
			float angle;
			
			//mainAxisの角度
			d = targetPos - mainAxis.transform.position;
			angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
			mainAxis.SetAngle(angle);

			//武器の角度
			foreach (var w in weapons) {
				d = targetPos - w.transform.position;
				angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
				w.SetAngle(angle);
			}
		}

		/// <summary>
		/// 装備を有効化
		/// </summary>
		public void Activate() {
			mainAxis.IsLocal = false;
			foreach (var w in weapons) {
				w.Activate();
			}
		}

		/// <summary>
		/// 装備を無効化
		/// </summary>
		public void Disactivate() {
			mainAxis.IsLocal = true;
			mainAxis.SetAngle(0f);
			foreach (var w in weapons) {
				w.Disactivate();
			}
		}
	
		#endregion
	}
}