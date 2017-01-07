using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ShootingUtility.ObjectDetector {

	/// <summary>
	/// オブジェクト検出器
	/// 特定のComponentを持つGameObjectを検出する
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public abstract class ObjectDetector2D<T> : MonoBehaviour where T : Component {

		public class DetectorEvent : UnityEvent<DetectableObject2D<T>> { }

		//検出関連
		private Collider2D detectionArea;						//検出領域
		private HashSet<DetectableObject2D<T>> objects;			//検出物

		//コールバック
		private DetectorEvent onDetect;		//検知
		public DetectorEvent OnDetect { get { return onDetect; } }
		private DetectorEvent onRelease;	//解除
		public DetectorEvent OnRelease { get { return onRelease; } }

		#region UnityEvent

		private void Awake() {
			detectionArea = GetComponent<Collider2D>();
			objects = new HashSet<DetectableObject2D<T>>();

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
		public void DetectObject(DetectableObject2D<T> obj) {
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
		public void ReleaseObject(DetectableObject2D<T> obj) {
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

		/// <summary>
		/// 最も近いオブジェクトを取得
		/// </summary>
		public DetectableObject2D<T> GetNearObject() {
			if (objects.Count == 0) {
				return null;
			} else if (objects.Count == 1) {
				return objects.First();
			} else {
				DetectableObject2D<T> nearObj = null;
				Vector3 pos = transform.position;
				float distA = float.MaxValue;
				float distB;
				foreach (var obj in objects) {
					distB = (obj.transform.position - pos).magnitude;
					if (distB < distA) {
						distA = distB;
						nearObj = obj;
					}
				}
				return nearObj;
			}
		}

		#endregion

	}
}