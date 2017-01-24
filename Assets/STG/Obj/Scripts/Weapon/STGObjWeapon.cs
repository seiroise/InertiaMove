using UnityEngine;
using System;
using STG.Obj.Equipment;
using STG.BaseUtility.ObjectDetector;
using STG.BaseUtility.ComSystem;

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

		//[Header("弾")]



		private float tInterval;

		#region UnityEvent

		private void Update() {
			if (isAwaked) {
				Reload();
			}
		}

		#endregion

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			tInterval = 0f;
		}

		#endregion

		#region Function

		/// <summary>
		/// 再装填
		/// </summary>
		private void Reload() {
			tInterval += Time.deltaTime;
			if (tInterval > baseParameter.Interval) {
				Shot();
				tInterval = 0f;
			}
		}

		/// <summary>
		/// 発射
		/// </summary>
		private void Shot() {
			
		}

		#endregion
	}
}