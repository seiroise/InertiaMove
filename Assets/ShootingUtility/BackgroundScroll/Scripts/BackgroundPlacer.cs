using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ShootingUtility.ObjectPool;

namespace ShootingUtility.BackgroundScroll {

	/// <summary>
	/// targetTransの移動に合わせてオブジェクトを指定の間隔で配置する
	/// </summary>
	public class BackgroundPlacer : MonoBehaviour {

		[SerializeField]
		protected BackgroundPool pool;
		[SerializeField]
		protected Transform target;
		public Transform Target { set { target = value; } }
		[SerializeField]
		protected Vector2 interval;

		private TrackingUVScroll nowObject;	//現在の配置オブジェクト
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

			var obj = pool.GetObject();
			obj.transform.position = new Vector3(x * interval.x, y * interval.y);
			obj.SetTargetTrans(target);

			nowObject = obj;
			nowX = x;
			nowY = y;
		}

		#endregion
	}
}