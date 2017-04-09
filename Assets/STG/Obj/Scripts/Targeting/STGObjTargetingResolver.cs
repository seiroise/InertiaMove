using UnityEngine;
using System;
using STG.Obj.Targeting;
using STG.Obj.Weapon;
using STG.Obj.Armor;
using STG.BaseUtility.ObjectDetector;
using STG.BaseUtility.ComSystem;
using STG.BaseUtility.Attack;
using STG.Obj.Sensor;

namespace STG.Obj.Targeting {

	/// <summary>
	/// STGオブジェクト用の目標解決器
	/// </summary>
	public class STGObjTargetingResolver : STGCom {

		[Header("検出関連")]
		[SerializeField]
		private STGObjDetector detector;		//ターゲット検出器
		public STGObjDetector Detector { get { return detector; } }

		[Header("ターゲッティング関連")]
		[SerializeField, Range(0.1f, 2f)]
		private float updateInterval = 0.4f;	//ターゲット更新間隔
		private float tUpdateInterval;
		[SerializeField]
		private bool onDetectUpdate = true;		//ターゲット検出/解除時にもターゲットの更新

		private STGObjWeaponController weaponCon;
		private STGObjArmor armor;

		#region UnityEvent

		private void Update() {
			UpdateTargeting();
		}

		#endregion

		#region VirtualFunction

		/// <summary>
		/// 起動
		/// </summary>
		public override void STGAwake() {
			base.STGAwake();
			tUpdateInterval = 0f;
			weaponCon = manager.GetCom<STGObjWeaponController>();
			if (weaponCon) {
				weaponCon.onSet.RemoveListener(OnSetWeapon);
				weaponCon.onSet.AddListener(OnSetWeapon);
			}
			armor = manager.GetCom<STGObjArmor>();
			if (armor) {
				armor.armor.OnDied.RemoveListener(OnDied);
				armor.armor.OnDied.AddListener(OnDied);
			}
			if (detector) {
				detector.OnDetect.RemoveListener(OnObjDetect);
				detector.OnDetect.AddListener(OnObjDetect);
				detector.OnRelease.RemoveListener(OnObjRelease);
				detector.OnRelease.AddListener(OnObjRelease);
			}
		}

		#endregion

		#region Function

		/// <summary>
		/// ターゲッティングの更新
		/// </summary>
		private void UpdateTargeting() {
			tUpdateInterval += Time.deltaTime;
			if (tUpdateInterval > updateInterval) {
				Targeting();
			}
		}

		/// <summary>
		/// ターゲッティング
		/// </summary>
		private void Targeting() {
			if (!weaponCon) return;
			//それぞれの武器毎に設定されている属性で一番近いオブジェクト
			weaponCon.EquipmentIterator((i, e) => {
				var t = detector.GetNearObject(e.TargetAttribute);
				if (t) {
					weaponCon.SetTarget(i, t.transform);
				} else {
					weaponCon.SetTarget(i, null);
				}
			});
		}

		#endregion

		#region Callback

		/// <summary>
		/// オブジェクトの検出
		/// </summary>
		private void OnObjDetect(ObjectAttribute attr, STGObj obj) {
			Targeting();
		}

		/// <summary>
		/// オブジェクトの解放
		/// </summary>
		private void OnObjRelease(ObjectAttribute attr, STGObj obj) {
			Targeting();
		}

		/// <summary>
		/// 武器が装備された時
		/// </summary>
		private void OnSetWeapon(int i, STGObjWeapon weapon) {
			Targeting();
		}

		/// <summary>
		/// 死亡時
		/// </summary>
		private void OnDied(AttackableObject2D attackable, ObjectAttacker2D attacker) {
			if (detector) {
				detector.ReleaseAllObject();
			}
		}

		#endregion
	}
}