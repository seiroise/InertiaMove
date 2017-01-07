using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingUtility.ObjectPool;

namespace ShootingUtility.BackgroundScroll {

	/// <summary>
	/// targetTransの移動に合わせてUVスクロールを行う
	/// </summary>
	public class TrackingUVScroll : MonoBehaviour, IPoolable {

		[SerializeField]
		private Transform targetTrans;
		[SerializeField]
		private Material targetMat;
		[SerializeField]
		private string texProp = "_MainTex";
		[SerializeField]
		private Vector2 baseScrollPos;
		[SerializeField, Range(-10f, 10f)]
		private float scrollScale = 1f;

		private Vector2 prevPos;	//前フレームでの位置
		private Vector2 sumDelta;	//差分の合計

		#region UnityEvent

		private void Start() {
			if (targetTrans) {
				prevPos = targetTrans.position;
			}
		}

		private void Update() {
			if (targetTrans) {
				sumDelta += new Vector2(targetTrans.position.x - prevPos.x, targetTrans.position.y - prevPos.y) * scrollScale;
				prevPos = targetTrans.position;
				targetMat.SetTextureOffset(texProp, baseScrollPos + sumDelta);
			}
		}

		#endregion

		#region Function

		/// <summary>
		/// targetTransの設定
		/// </summary>
		public void SetTargetTrans(Transform targetTrans) {
			this.targetTrans = targetTrans;
			Start();
		}

		#endregion

		#region IObjectPoolable

		/// <summary>
		/// オブジェクトの初期化
		/// </summary>
		public void InitPoolable() {}

		#endregion
	}
}