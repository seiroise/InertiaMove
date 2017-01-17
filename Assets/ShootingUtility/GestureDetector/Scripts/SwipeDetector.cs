using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace ShootingUtility.GestureDetector {

	/// <summary>
	/// スワイプイベントデータ
	/// </summary>
	[Serializable]
	public struct SwipeEventData {

		public SwipeDetector detector;
		public Vector2 delta;
		public float deltaMag;

		public SwipeEventData(SwipeDetector detector, Vector2 delta, float deltaMag) {
			this.detector = detector;
			this.delta = delta;
			this.deltaMag = deltaMag;
		}
	}

	/// <summary>
	/// スワイプイベント
	/// </summary>
	public class SwipeEvent : UnityEvent<SwipeEventData> { }

	/// <summary>
	/// スワイプ検出
	/// </summary>
	public class SwipeDetector : MonoBehaviour {

		[Header("検出距離")]
		[SerializeField, Range(0f, 5f)]
		private float detectDistance = 0.1f;
		[Header("デバッグ用")]
		[SerializeField]
		private bool isDebug;

		private int basePixel = 720;
		public int BasePixel { get { return basePixel; } }

		//コールバック
		private SwipeEvent onStartedSwipe;
		public SwipeEvent OnStartedSwipe { get { return onStartedSwipe; } }

		private SwipeEvent onSwiping;
		public SwipeEvent OnSwiping { get { return onSwiping; } }

		private SwipeEvent onEndedSwipe;
		public SwipeEvent OnEndedSwipe { get { return onEndedSwipe; } }

		//その他パラメータ
		private Vector2 from;
		public Vector2 From { get { return from; } }

		private Vector2 to;
		public Vector2 To { get { return to; } }

		private Vector2 delta;
		public Vector2 Delta { get { return delta; } }

		private float deltaMag;
		public float DeltaMag { get { return deltaMag; } }

		private float angle;
		public float Angle { get { return angle; } }

		private bool isTouching;
		private bool isSwiping;

		#region UnityEvent

		private void Awake() {
			onStartedSwipe = new SwipeEvent();
			onSwiping = new SwipeEvent();
			onEndedSwipe = new SwipeEvent();

			basePixel = Mathf.Min(Screen.width, Screen.height);
		}

		private void Update() {
			DetectSwipe();
		}

		#endregion

		#region Function

		/// <summary>
		/// スワイプ検出
		/// </summary>
		private void DetectSwipe() {
			if (Input.GetMouseButtonDown(0)) {
				SwipeProc();
				from = to;
			} else if (Input.GetMouseButtonUp(0)) {
				if (isSwiping) {
					isSwiping = false;
					SwipeProc();
					onEndedSwipe.Invoke(new SwipeEventData(this, delta, deltaMag));
				}
			} else if (Input.GetMouseButton(0)) {
				SwipeProc();
				if (isDebug) Debug.Log(deltaMag);
				if (deltaMag > detectDistance) {
					if (!isSwiping) {
						isSwiping = true;
						SwipeProc();
						onStartedSwipe.Invoke(new SwipeEventData(this, delta, deltaMag));
					} else {
						SwipeProc();
						onSwiping.Invoke(new SwipeEventData(this, delta, deltaMag));
					}
				}
			}
		}

		/// <summary>
		/// データの処理
		/// </summary>
		private void SwipeProc() {
			to = Input.mousePosition / basePixel;
			delta = to - from;
			deltaMag = delta.magnitude;
			angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
		}

		#endregion
	}
}