using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections.Generic;

namespace STG.BaseUtility.ObjectDetector {

	/// <summary>
	/// オブジェクト検出器
	/// 特定のComponentを持つGameObjectを検出する
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public abstract class ObjectDetector2D<T> : MonoBehaviour where T : Component {

		public class DetectorEvent : UnityEvent<ObjectAttribute, T> { }

		//検出関連
		private Collider2D detectArea;						//検出領域
		private HashSet<DetectableObject2D<T>> objects;		//検出物

		private Dictionary<ObjectAttribute, HashSet<DetectableObject2D<T>>> objectDic;	//検出物辞書

		//コールバック
		private DetectorEvent onDetect;		//検知
		public DetectorEvent OnDetect { get { return onDetect; } }
		private DetectorEvent onRelease;	//解除
		public DetectorEvent OnRelease { get { return onRelease; } }

		#region UnityEvent

		private void Awake() {
			detectArea = GetComponent<Collider2D>();
			objects = new HashSet<DetectableObject2D<T>>();
			objectDic = new Dictionary<ObjectAttribute, HashSet<DetectableObject2D<T>>>();

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
			if (!objectDic.ContainsKey(obj.attribute)) {
				objectDic.Add(obj.attribute, new HashSet<DetectableObject2D<T>>());
			}
			objectDic[obj.attribute].Add(obj);
			objects.Add(obj);
			//オブジェクト側も追加
			obj.DetectDetector(this);
			//コールバック
			onDetect.Invoke(obj.attribute, obj.component);
		}

		/// <summary>
		/// オブジェクトの解除
		/// </summary>
		public void ReleaseObject(DetectableObject2D<T> obj) {
			//包含判定
			if (!objects.Contains(obj)) return;
			//削除
			objectDic[obj.attribute].Remove(obj);
			objects.Remove(obj);
			//オブジェクト側も削除
			obj.ReleaseDetector(this);
			//コールバック
			onRelease.Invoke(obj.attribute, obj.component);
		}

		/// <summary>
		/// 全てのオブジェクトの解除
		/// </summary>
		public void ReleaseAllObject() {
			foreach (var o in objects.Reverse()) {
				ReleaseObject(o);
			}
			objects.Clear();
			objectDic.Clear();
		}

		/// <summary>
		/// 検出オブジェクトの数を取得
		/// </summary>
		public int GetDetectCount() {
			return objects.Count;
		}

		/// <summary>
		/// 検出範囲の取得
		/// </summary>
		public float GetDetectAreaSize() {
			//ガバガバ
			return detectArea.transform.localScale.x;
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

		/// <summary>
		/// 指定した属性で最も近いオブジェクトを取得
		/// </summary>
		public T GetNearObject(ObjectAttribute attr) {
			if (!objectDic.ContainsKey(attr)) return null;
			var list = objectDic[attr];
			int count = list.Count;
			//起こりそうな状況順
			if(count > 1) {
				DetectableObject2D<T> nearObj = null;
				Vector3 pos = transform.position;
				float distA = float.MaxValue;
				float distB;
				foreach (var obj in list) {
					distB = (obj.transform.position - pos).magnitude;
					if (distB < distA) {
						distA = distB;
						nearObj = obj;
					}
				}
				return nearObj.component;
			} else if (count == 0) {
				return null;
			} else {
				return list.First().component;
			}
		}

		/// <summary>
		/// 待機状態へ
		/// </summary>
		public void Standby() {
			detectArea.enabled = false;
			ReleaseAllObject();
		}

		#endregion
	}
}