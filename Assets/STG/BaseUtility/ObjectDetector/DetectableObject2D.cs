using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections.Generic;

namespace STG.BaseUtility.ObjectDetector {

	/// <summary>
	/// 検出可能オブジェクト
	/// ObjectDetectorと基本的にはセット
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public abstract class DetectableObject2D<T> : MonoBehaviour where T : Component {

		public class DetectedEvent : UnityEvent<ObjectDetector2D<T>> { }

		//検出関連
		private Collider2D _detectableArea;
		private HashSet<ObjectDetector2D<T>> _detectors;

		[SerializeField]
		private ObjectAttribute _attribute;
		public ObjectAttribute attribute { get { return _attribute; } }

		[SerializeField]
		private T _component;	//ObjectDetectorに検出させたいコンポーネント
		public T component { get { return _component; } }

		//コールバック
		private DetectedEvent _onDetected;  //検知された
		public DetectedEvent onDetected { get { return _onDetected; } }
		private DetectedEvent _onReleased;  //解除された
		public DetectedEvent onReleased { get { return _onReleased; } }

		#region UnityEvent

		private void Awake() {
			_detectableArea = GetComponent<Collider2D>();
			_detectors = new HashSet<ObjectDetector2D<T>>();

			_onDetected = new DetectedEvent();
			_onReleased = new DetectedEvent();
		}

		private void OnTriggerEnter2D(Collider2D co) {
			var detector = co.GetComponent<ObjectDetector2D<T>>();
			if(detector) {
				DetectDetector(detector);
			}
		}

		private void OnTriggerExit2D(Collider2D co) {
			var detector = co.GetComponent<ObjectDetector2D<T>>();
			if(detector) {
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
		public void DetectDetector(ObjectDetector2D<T> detector) {
			//包含確認
			if(_detectors.Contains(detector)) return;
			detector.DetectObject(this);
			//detector側も追加
			_detectors.Add(detector);
			//コールバック
			_onDetected.Invoke(detector);
		}

		/// <summary>
		/// detectorの解除
		/// </summary>
		public void ReleaseDetector(ObjectDetector2D<T> detector) {
			//包含確認
			if(!_detectors.Contains(detector)) return;
			detector.ReleaseObject(this);
			//detector側も解除
			_detectors.Remove(detector);
			//コールバック
			_onReleased.Invoke(detector);
		}

		/// <summary>
		/// 全てのDetectorの解除
		/// </summary>
		public void ReleaseAllDetector() {
			foreach(var d in _detectors.Reverse()) {
				ReleaseDetector(d);
			}
			_detectors.Clear();
		}

		/// <summary>
		/// 待機状態へ
		/// </summary>
		public void Standby() {
			_detectableArea.enabled = false;
			ReleaseAllDetector();
		}

		#endregion
	}
}