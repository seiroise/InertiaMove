using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ShootingUtility.ObjectDetector {

	public class DetectorEvent : UnityEvent<DetectableObject2D> { }

	/// <summary>
	/// オブジェクト検出器
	/// 特定のComponentを持つGameObjectを検出する
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public class ObjectDetector2D : MonoBehaviour {

		//検出関連
		private Collider2D detectionArea;						//検出領域
		private HashSet<DetectableObject2D> objects;			//検出物
		public HashSet<DetectableObject2D> Objects { get { return objects; } }

		//コールバック
		private DetectorEvent onDetect;		//検知
		public DetectorEvent OnDetect { get { return onDetect; } }
		private DetectorEvent onRelease;	//解除
		public DetectorEvent OnRelease { get { return onRelease; } }

		#region UnityEvent

		private void Awake() {
			detectionArea = GetComponent<Collider2D>();
			objects = new HashSet<DetectableObject2D>();

			onDetect = new DetectorEvent();
			onRelease = new DetectorEvent();
		}

		private void OnDestroy() {
			ReleaseAllObject();
		}

		#endregion

		#region Function

		/// <summary>
		/// オブジェクトの検出
		/// </summary>
		public void DetectObject(DetectableObject2D obj) {
			//包含判定
			if (objects.Contains(obj)) return;
			//検出オブジェクトの追加
			objects.Add(obj);
			//オブジェクト側も追加
			obj.DetectDetector(this);

			//コールバック
			onDetect.Invoke(obj);
		}

		/// <summary>
		/// オブジェクトの解除
		/// </summary>
		public void ReleaseObject(DetectableObject2D obj) {
			//包含判定
			if (!objects.Contains(obj)) return;
			//削除
			objects.Remove(obj);
			//オブジェクト側も削除
			obj.ReleaseDetector(this);

			//コールバック
			onRelease.Invoke(obj);
		}

		/// <summary>
		/// 全てのオブジェクトの解除
		/// </summary>
		public void ReleaseAllObject() {
			foreach (var o in objects.Reverse()) {
				ReleaseObject(o);
			}
			objects.Clear();
		}

		/// <summary>
		/// 検出オブジェクトの数を取得
		/// </summary>
		public int GetDetectCount() {
			return objects.Count;
		}

		#endregion

	}
}