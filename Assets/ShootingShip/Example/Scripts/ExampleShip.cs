using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Structure;
using ShootingShip.Utility;
using ShootingUtility.InputDetector;
using ShootingUtility.ObjectDetector;
using ShootingShip.Manager;
using ShootingShip.Attacker;
using ShootingUtility.ValueIndicator;

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
		private ObjectDetector2D detector;

		//値の表示
		private AttackableObject2D attackable;
		private FloatIndicator indicator;

		//ターゲッティング
		private Transform trans;

		#region UnityEvent

		private void Start() {
			tag = "Player";
			trans = transform;
			StageManager sManager = StageManager.Instance;
			if (sManager) {
				sManager.PlayerTracker.Target = trans;
				sManager.BgPlacer.Target = trans;
				swipe = sManager.Swipe;
				if (swipe) {
					swipe.OnSwiping.RemoveListener(OnSwiping);
					swipe.OnSwiping.AddListener(OnSwiping);
					swipe.OnStartedSwipe.RemoveListener(OnStartedSwipe);
					swipe.OnStartedSwipe.AddListener(OnStartedSwipe);
					swipe.OnEndedSwipe.RemoveListener(OnEndedSwipe);
					swipe.OnEndedSwipe.AddListener(OnEndedSwipe);
				}
				flick = sManager.Flick;
				if(flick) {
					flick.OnFlick.RemoveListener(OnFlick);
					flick.OnFlick.AddListener(OnFlick);
				}
			}
		}

		private void Update() {
			SetTargetAngle();
		}

		#endregion

		#region Function

		/// <summary>
		/// 機体構造の設定
		/// </summary>
		public void SetStructure(ShipStructure structure) {
			weapon = structure.WeaponController;
			if(weapon) {
				weapon.SetBulletExclusionTag("Player");
			}
			thruster = structure.ThrusterController;
			attitude = structure.AttitudeController;
			marker = structure.Marker;
			if (marker) {
				detector = marker.ObjDetector;
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
		}

		/// <summary>
		/// 対象との角度の調整
		/// </summary>
		private void SetTargetAngle() {
			if (detector && detector.GetDetectCount() > 0) {
				//距離計算
				float d1 = 0f, d2 = float.MaxValue;
				DetectableObject2D near = null;
				foreach (var obj in detector.Objects) {
					d1 = Vector2.Distance(trans.position, obj.transform.position);
					if (d1 < d2) {
						d2 = d1;
						near = obj;
					}
				}
				weapon.SetTargetAngle(near.transform);
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

		private void OnFlick(FlickEventData ev) {
			if(thruster) {
			}
		}

		/// <summary>
		/// 他のオブジェクトを検出
		/// </summary>
		private void OnDetect(DetectableObject2D obj) {
			//武器を起動
			if (weapon) {
				weapon.WeaponAwake();
			}
		}

		/// <summary>
		/// 他のオブジェクトを解放
		/// </summary>
		private void OnRelease(DetectableObject2D obj) {
			//武器を待機
			if (weapon) {
				weapon.SetAngle(0f);
				if (detector.GetDetectCount() < 1) {
					weapon.WeaponStandby();
				}
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
				var sManager = StageManager.Instance;
				if (sManager && sManager.IndicatorPool) {
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