using UnityEngine;
using System.Collections.Generic;

namespace ShootingStage.Object {

	/// <summary>
	/// StageObjectの配置
	/// </summary>
	public class StageObjectLocater : MonoBehaviour {

		[SerializeField]
		private StageObject[] stageObjects;
		[SerializeField, Range(0, 20)]
		private int locateNum = 5;
		[SerializeField, Range(0f, 500f)]
		private float locateDensity = 100f;

		#region UnityEvent

		private void Start() {
			//テスト配置
			Locate(locateNum, locateDensity);
		}

		#endregion

		#region Function

		/// <summary>
		/// 配置
		/// </summary>
		private void Locate(int num, float density) {
			Vector3 pos = Vector3.zero;
			for (int i = 0; i < num; ++i) {
				//座標を求める
				pos += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * density;
				//ランダムに生成
				var obj = Instantiate(stageObjects[Random.Range(0, stageObjects.Length)]);
				//取り敢えず親
				obj.transform.SetParent(transform);
				obj.transform.position = pos;
			}
		}

		#endregion
	}
}