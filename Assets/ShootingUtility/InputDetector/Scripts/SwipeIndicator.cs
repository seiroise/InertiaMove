using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingUtility.InputDetector {

	/// <summary>
	/// SwipeDetectorの情報を可視化する
	/// </summary>
	public class SwipeIndicator : MonoBehaviour {

		public SwipeDetector swipe;
		public RectTransform canvas;
		public RectTransform arrow;

		private int basePixel;

		#region UnityEvent

		private void Start() {
			swipe.OnStartedSwipe.AddListener(OnStartedSwipe);
			swipe.OnSwiping.AddListener(OnSwiping);
			swipe.OnEndedSwipe.AddListener(OnEndedSwipe);

			basePixel = (int)Mathf.Min(canvas.sizeDelta.x, canvas.sizeDelta.y);
		}

		#endregion

		#region Callback

		private void OnStartedSwipe(SwipeEventData ev) {
			arrow.gameObject.SetActive(true);
			//座標を求める
			Vector3 pos = ev.detector.From * basePixel;
			arrow.transform.localPosition = pos;
		}

		private void OnSwiping(SwipeEventData ev) {
			//角度
			arrow.eulerAngles = new Vector3(0f, 0f, ev.detector.Angle);
			//大きさ
			Vector2 delta = ev.delta * basePixel;
			arrow.sizeDelta = new Vector2(delta.magnitude, 10f);
		}

		private void OnEndedSwipe(SwipeEventData ev) {
			arrow.gameObject.SetActive(false);
		}

		#endregion
	}
}