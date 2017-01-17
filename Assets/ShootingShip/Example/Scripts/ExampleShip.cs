using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Structure;
using ShootingShip.Utility;
using ShootingUtility.GestureDetector;
using ShootingUtility.ObjectDetector;
using ShootingShip.Manager;
using ShootingShip.Attacker;
using ShootingUtility.ValueIndicator;
using ShootingUI.Manager;

namespace ShootingShip.Example {

	/// <summary>
	/// 機体例
	/// </summary>
	[RequireComponent(typeof(ShipStructure))]
	public class ExampleShip : MonoBehaviour {

		private SwipeDetector swipe;
		private FlickDetector flick;

		//コンポーネント群
		private ShipWeaponController weapon;
		private ShipThrusterController thruster;
		private ShipAttitudeController attitude;
		private ShipMarker marker;

		//値の表示
		private AttackableObject2D attackable;
		private FloatIndicator indicator;

		//ターゲッティング
		private RigidbodyDetector2D detector;
		private Transform targetTrans;
		private DetectableObject2D<Rigidbody2D> detected;

		#region UnityEvent

		private void Start() {
			tag = "Player";
			targetTrans = transform;
			ShipManager sManager = ShipManager.Instance;
			if (sManager) {
				sManager.PlayerTracker.Target = targetTrans;
				sManager.BgPlacer.Target = targetTrans;
				swipe = sManager.Swipe;
				if (swipe) {
					swipe.OnSwiping.RemoveListener(OnSwiping);
					swipe.OnSwiping.AddListener(OnSwiping);
					swipe.OnStartedSwipe.RemoveListener(OnStartedSwipe);
					swipe.OnStartedSwipe.AddListener(OnStartedSwipe);
					swipe.OnEndedSwipe.RemoveListener(OnEndedSwipe);
					swipe.OnEndedSwipe.AddListener(OnEndedSwipe);
				}
			}
		}

		private void Update() {
			Targeting();
		}

		#endregion

		#region Function

		/// <summary>
		/// 機体構造の設定
		/// </summary>
		public void SetStructure(ShipStructure structure) {
			weapon = structure.WeaponController;
			if (weapon) {
				weapon.SetBulletExclusionTag("Player");
			}
			thruster = structure.ThrusterController;
			attitude = structure.AttitudeController;
			marker = structure.Marker;
			if (marker) {
				detector = marker.Detector;
				detector.OnDetect.RemoveListener(OnDetect);
				detector.OnDetect.AddListener(OnDetect);
				detector.OnRelease.RemoveListener(OnRelease);
				detector.OnRelease.AddListener(OnRelease);
			}
			attackable = structure.Attackable;
			if (attackable) {
				attackable.OnAttacked.RemoveListener(OnAttacked);
				attackable.OnAttacked.AddListener(OnAttacked);
				attackable.OnDied.RemoveListener(OnDied);
				attackable.OnDied.AddListener(OnDied);
			}
			//UI
			UIManager.Instance.ShipQuickBar.SetQuickBar(structure);
		}

		/// <summary>
		/// ターゲッティング
		/// </summary>
		private void Targeting() {
			if (detector) {
				if(detected = detector.GetNearObject()) {
					weapon.SetTarget(detected.DetectableObj);
				}
			}
		}

		#endregion

		#region Callback

		/// <summary>
		/// スワイプ中
		/// </summary>
		private void OnSwiping(SwipeEventData ev) {
			//角度の設定
			if (attitude) {
				attitude.Go(ev.detector.Angle);
			}
		}

		/// <summary>
		/// スワイプ開始
		/// </summary>
		private void OnStartedSwipe(SwipeEventData ev) {
			if (thruster) {
				thruster.ThrusterAwake();
			}
		}

		/// <summary>
		/// スワイプ終了
		/// </summary>
		private void OnEndedSwipe(SwipeEventData ev) {
			if (thruster) {
				thruster.ThrusterStandby();
				thruster.SetThrusterAngle(0f);
			}
		}

		/// <summary>
		/// 他のオブジェクトを検出
		/// </summary>
		private void OnDetect(DetectableObject2D<Rigidbody2D> obj) {
			//武器を起動
			if (weapon && detector.GetDetectCount() > 0) {
				weapon.WeaponAwake();
			}
		}

		/// <summary>
		/// 他のオブジェクトを解放
		/// </summary>
		private void OnRelease(DetectableObject2D<Rigidbody2D> obj) {
			//武器を待機
			if (weapon && detector.GetDetectCount() <= 0) {
				weapon.WeaponStandby();
			}
		}

		/// <summary>
		/// 攻撃された
		/// </summary>
		private void OnAttacked(ObjectAttacker2D attacker) {
			if (indicator) {
				//値の反映
				indicator.SetRatio(attackable.HP, attackable.NowHP);
			} else {
				//表示器の取得
				var sManager = ShipManager.Instance;
				if (sManager && sManager.IndicatorPool) {
					indicator = sManager.IndicatorPool.GetObject();
					indicator.Tracker.Target = targetTrans;
					indicator.SetRatio(attackable.HP, attackable.NowHP);
				}
			}
		}

		/// <summary>
		/// 倒された
		/// </summary>
		private void OnDied(AttackableObject2D attackable, ObjectAttacker2D attacker) {
			indicator.gameObject.SetActive(false);
			Destroy(gameObject);
		}

		#endregion
	}
}