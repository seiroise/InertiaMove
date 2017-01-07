using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingUtility.ObjectDetector;
using InertiaShooting.Weapon;
using ShootingUtility.Lerp;

namespace InertiaShooting {

	/// <summary>
	/// プレイヤー
	/// </summary>
	public class Player : Character {

		[Header("装備")]
		[SerializeField]
		private WeaponManager wManager;

		[Header("検出関連")]
		[SerializeField]
		private ObjectDetector2D detector;

		[Header("ターゲッティング")]
		[SerializeField]
		private DetectableObject2D target;
		private Transform targetTrans;

		[Header("その他")]
		[SerializeField]
		private LerpCameraSize cameraSize;

		#region UnityEvent

		private void Start() {
			InitPoolable();
		}

		private void Update() {
			Targeting();
		}

		#endregion

		#region IPoolable

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitPoolable() {
			base.InitPoolable();

			if (detector) {
				detector.OnDetect.RemoveAllListeners();
				detector.OnDetect.AddListener(OnDetect);
				detector.OnRelease.RemoveAllListeners();
				detector.OnRelease.AddListener(OnRelease);
			}

			wManager.Disactivate();
		}

		#endregion

		#region Function

		/// <summary>
		/// ターゲッティング
		/// </summary>
		private void Targeting() {
			if (target) {
				wManager.UpdateAngle(targetTrans);
			}
		}

		#endregion

		#region Callback

		/// <summary>
		/// 検出
		/// </summary>
		private void OnDetect(DetectableObject2D obj) {
			Debug.Log("OnDetect : " + obj);
			//ターゲッティングの優先順位の更新

			//カメラサイズの変更
			if (cameraSize) {
				cameraSize.SetCameraSize(detector.GetDetectCount() * 1f + 20f);
			}

			//仮
			target = obj;
			targetTrans = obj.transform;
			wManager.Activate();
		}

		/// <summary>
		/// 解除
		/// </summary>
		private void OnRelease(DetectableObject2D obj) {
			Debug.Log("OnRelease : " + obj);
			//ターゲッティングの優先順位の更新

			//仮
			if (target == obj) {
				target = null;
			}
			wManager.Disactivate();
		}

		#endregion
	}
}