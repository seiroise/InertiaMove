using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace ShootingUtility.GestureDetector {

	/// <summary>
	/// フリックイベントデータ
	/// </summary>
	[Serializable]
	public struct FlickEventData {

		public FlickDetector detector;
		public Vector2 value;
		public float magnitude;

		public FlickEventData(FlickDetector detector, Vector2 value, float magnitude) {
			this.detector = detector;
			this.value = value;
			this.magnitude = magnitude;
		}
	}

	/// <summary>
	/// フリックイベント
	/// </summary>
	public class FlickEvent : UnityEvent<FlickEventData> { }

	/// <summary>
	/// 過去の入力情報
	/// </summary>
	[Serializable]
	public struct PastInputData {

		public float deltaTime;
		public Vector2 delta;
		public Vector2 VelocityPerSec { get { return delta / deltaTime; } }

		public PastInputData(float deltaTime, Vector2 delta) {
			this.deltaTime = deltaTime;
			this.delta = delta;
		}
	}

	/// <summary>
	/// フリック検出
	/// </summary>
	public class FlickDetector : MonoBehaviour {

		[Header("基準ピクセル数")]
		[SerializeField]
		[Range(0, 1920)]
		private int basePixel = 720;

		[Header("検出閾値")]
		[SerializeField]
		[Range(0f, 100f)]
		private float threshold = 0.5f;

		[Header("判定フレーム数")]
		[SerializeField]
		[Range(1, 60)]
		private int detectFrameCount = 4;

		public bool isDebug;

		//座標記録関連
		private PastInputData[] pastInputs;	//過去の入力情報
		private int startIndex;				//座標逆計算開始位置
		private int posCount;				//記録座標数
		private Vector2 prevPos;			//直近の入力座標

		//コールバック
		private FlickEvent onFlick;
		public FlickEvent OnFlick { get { return onFlick; } }

		#region UnityEvent

		private void Awake() {

			onFlick = new FlickEvent();

			pastInputs = new PastInputData[detectFrameCount];
			Reset();
		}

		private void Update() {
			if (Input.GetMouseButtonDown(0)) {
				prevPos = Input.mousePosition / basePixel;
			} else if (Input.GetMouseButtonUp(0)) {
				//フリック検出
				Vector2 sum = SumReverseDelta();
				float mag = sum.magnitude;
				if (isDebug) Debug.Log(sum + " : " + mag);
				if (mag > threshold) {
					onFlick.Invoke(new FlickEventData(this, sum, mag));
				}
				Reset();
			} else if (Input.GetMouseButton(0)) {
				AddInputData();
			}
		}

		#endregion

		#region Function

		/// <summary>
		/// 値のリセット
		/// </summary>
		private void Reset() {
			startIndex = 0;
			posCount = 0;
		}

		/// <summary>
		/// 座標の追加
		/// </summary>
		private void AddInputData() {
			//直近のフレームからの差分座標を求める
			Vector2 pos = Input.mousePosition / basePixel;
			pastInputs[startIndex] = new PastInputData(Time.deltaTime, pos - prevPos);
			prevPos = pos;
			startIndex = (startIndex + 1) % detectFrameCount;
			if (posCount < detectFrameCount) posCount++;
		}

		/// <summary>
		/// 差分を逆向きに合計する
		/// </summary>
		private Vector2 SumReverseDelta() {
			Vector2 sum = Vector2.zero;
			for (int i = 0; i < posCount; ++i) {
				sum += pastInputs[startIndex].VelocityPerSec;
				startIndex = (startIndex + detectFrameCount - 1) % detectFrameCount;
			}
			return sum / detectFrameCount;
		}

		#endregion

	}
}