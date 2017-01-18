using UnityEngine;
using System;
using System.Collections.Generic;
using STG.BaseUtility.ObjectPool;

namespace STG.Utility.BGScroll {

	/// <summary>
	/// 目標の移動に合わせてUVスクロールを行う
	/// </summary>
	[AddComponentMenu("STG/Utility/BGScroll/TrackingUVScroll")]
	public class TrackingUVScroll : MonoBehaviour, IPoolable{

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
			SetTargetTrans(targetTrans);
		}

		private void Update() {
			UpdateUV();
		}

		#endregion

		#region Function

		/// <summary>
		/// targetTransの設定
		/// </summary>
		public void SetTargetTrans(Transform targetTrans) {
			this.targetTrans = targetTrans;
			if (targetTrans) {
				prevPos = targetTrans.position;
			}
		}

		/// <summary>
		/// UVの更新
		/// </summary>
		public void UpdateUV() {
			if (targetTrans && targetMat) {
				sumDelta += new Vector2(targetTrans.position.x - prevPos.x, targetTrans.position.y - prevPos.y) * scrollScale;
				prevPos = targetTrans.position;
				targetMat.SetTextureOffset(texProp, baseScrollPos + sumDelta);
			}
		}

		#endregion

		#region IPoolable

		/// <summary>
		/// プールオブジェクトの初期化
		/// </summary>
		public void InitPoolable() {
			//特にすることなし
		}

		#endregion
	}
}