using UnityEngine;
using System;
using ShootingShip.Structure;
using ShootingUtility.ObjectDetector;
using ShootingShip.Manager;
using ShootingShip.Attacker;
using ShootingUtility.ValueIndicator;

namespace ShootingShip.Example {

	/// <summary>
	/// AIっぽいリーダー機体
	/// </summary>
	[RequireComponent(typeof(ShipStructure))]
	public class AILeaderShip : MonoBehaviour {

		[SerializeField]
		private string targetTag;
		public string TargetTag { get { return targetTag; } set { targetTag = value; } }

		//コンポーネント軍
		private ShipWeaponController weapon;
		private ShipThrusterController thruster;
		private ShipAttitudeController attitude;
		private ShipMarker marker;
		private ObjectDetector2D detector;

		//ターゲッティング
		private Transform trans;
		private Transform target;

		//値の表示
		private AttackableObject2D attackable;
		private FloatIndicator indicator;

		//ステージマネージャ
		private StageManager sManager;

		#region UnityEvent

		private void Start() {
			tag = "Enemy";
			trans = transform;
		}

		private void Update() {
			if(target) {
				Targeting();
			}
		}

		#endregion

		#region Function

		/// <summary>
		/// 機体構造の設定
		/// </summary>
		public void SetStructure(ShipStructure structure) {
			weapon = structure.WeaponController;
			if(weapon) {
				weapon.SetBulletExclusionTag("Enemy");
			}
			thruster = structure.ThrusterController;
			attitude = structure.AttitudeController;
			marker = structure.Marker;
			if(marker) {
				detector = marker.ObjDetector;
				detector.OnDetect.RemoveListener(OnDetect);
				detector.OnDetect.AddListener(OnDetect);
			}
			attackable = structure.Attackable;
			if(attackable) {
				attackable.OnAttacked.RemoveListener(OnAttacked);
				attackable.OnAttacked.AddListener(OnAttacked);
				attackable.OnDied.RemoveListener(OnDied);
				attackable.OnDied.AddListener(OnDied);
			}
		}

		/// <summary>
		/// ターゲッティング
		/// </summary>
		private void Targeting() {
			//武器の角度変更
			if(weapon) {
				weapon.SetTargetAngle(target);
			}
			//進む
			if(attitude) {
				//角度
				Vector2 d = target.position - trans.position;
				float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
				attitude.Go(angle);
			}
		}

		#endregion

		#region Callback

		/// <summary>
		/// 他のオブジェクトを検出
		/// </summary>
		private void OnDetect(DetectableObject2D obj) {
			//フィルター
			if(obj.tag.Equals(targetTag)) {
				thruster.ThrusterAwake();
				target = obj.transform;
				weapon.WeaponAwake();
			}
		}

		/// <summary>
		/// 検出オブジェクトを解放
		/// </summary>
		private void OnRelease(DetectableObject2D obj) {
			if(obj.transform == target) {
				thruster.ThrusterStandby();
				target = null;
				weapon.WeaponStandby();
			}
		}

		/// <summary>
		/// 攻撃された
		/// </summary>
		private void OnAttacked(ObjectAttacker2D attacker) {
			if(indicator) {
				//値の反映
				indicator.SetRatio(attackable.HP, attackable.NowHP);
			} else {
				//表示器の取得
				var sManager = StageManager.Instance;
				if(sManager && sManager.IndicatorPool) {
					indicator = sManager.IndicatorPool.GetObject();
					indicator.Tracker.Target = trans;
					indicator.SetRatio(attackable.HP, attackable.NowHP);
				}
			}
		}

		/// <summary>
		/// 倒された
		/// </summary>
		private void OnDied(ObjectAttacker2D attacker) {
			indicator.gameObject.SetActive(false);
			Destroy(gameObject);
		}

		#endregion
	}
}