using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ShootingUtility.ObjectDetector {

	public class DetectedEvent : UnityEvent<ObjectDetector2D> { }

	/// <summary>
	/// 検出可能オブジェクト
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public class DetectableObject2D : MonoBehaviour {

		protected HashSet<ObjectDetector2D> detectors;

		//コールバック
		private DetectedEvent onDetected;	//検知された
		public DetectedEvent OnDetected { get { return onDetected; } }
		private DetectedEvent onReleased;	//解除された
		public DetectedEvent OnReleased { get { return onReleased; } }

		#region UnityEvent

		private void Awake() {
			detectors = new HashSet<ObjectDetector2D>();

			onDetected = new DetectedEvent();
			onReleased = new DetectedEvent();
		}

		private void OnTriggerEnter2D(Collider2D co) {
			var detector = co.GetComponent<ObjectDetector2D>();
			if (detector) {
				DetectDetector(detector);
			}
		}

		private void OnTriggerExit2D(Collider2D co) {
			var detector = co.GetComponent<ObjectDetector2D>();
			if (detector) {
				ReleaseDetector(detector);
			}
		}

		private void OnDestroy() {
			ReleaseAllDetector();
		}

		#endregion

		#region Function

		/// <summary>
		/// detectorの検出
		/// </summary>
		public void DetectDetector(ObjectDetector2D detector) {
			//包含確認
			if (detectors.Contains(detector)) return;
			detector.DetectObject(this);
			//detector側も追加
			detectors.Add(detector);

			//コールバック
			onDetected.Invoke(detector);
		}

		/// <summary>
		/// detectorの解除
		/// </summary>
		public void ReleaseDetector(ObjectDetector2D detector) {
			//包含確認
			if (!detectors.Contains(detector)) return;
			detector.ReleaseObject(this);
			//detector側も解除
			detectors.Remove(detector);

			//コールバック
			onReleased.Invoke(detector);
		}

		/// <summary>
		/// 全てのDetectorの解除
		/// </summary>
		public void ReleaseAllDetector() {
			foreach (var d in detectors.Reverse()) {
				ReleaseDetector(d);
			}
			detectors.Clear();
		}

		#endregion

	}
}