using UnityEngine;
using System;
using System.Collections.Generic;

namespace STG.Utility.BGScroll {

	[AddComponentMenu("STG/Utility/BGScroll/BGLocater")]
	public class BGLocater : MonoBehaviour {

		[SerializeField]
		private BGPool bgPool;
		[SerializeField]
		private string bgObjName;
		[SerializeField]
		private Transform target;
		public Transform Target { set { target = value; } }
		[SerializeField]
		private Vector2 interval;

		private BGObject nowObject;			//現在の配置オブジェクト
		private int nowX;					//現在の座標番号(x)
		private int nowY;					//現在の座標番号(y)

		#region UnityEvent

		private void Start() {
			UpdateTracking();
		}

		private void Update() {
			UpdateTracking();
		}

		#endregion

		#region Function

		/// <summary>
		/// 追跡の更新
		/// </summary>
		private void UpdateTracking() {
			if (target) {
				//配置座標番号を求める
				int x = Mathf.RoundToInt(target.position.x / interval.x);
				int y = Mathf.RoundToInt(target.position.y / interval.y);

				if (nowObject) {
					//オブジェクトが存在している場合
					if (nowX != x || nowY != y) {
						SetObject(x, y);
					}
				} else {
					//オブジェクトが存在していない場合
					SetObject(x, y);
				}
			}
		}

		/// <summary>
		/// オブジェクトの配置
		/// </summary>
		private void SetObject(int x, int y) {
			if (nowObject) {
				nowObject.gameObject.SetActive(false);
			}

			nowObject = bgPool.GetObject(bgObjName, new Vector3(x * interval.x, y * interval.y));
			nowX = x;
			nowY = y;
		}

		#endregion
	}
}