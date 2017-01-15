using UnityEngine;
using System.Collections;
using EditorUtility;
using ShootingStage.Object;

namespace ShootingStage.Locater {

	/// <summary>
	/// Objectの配置
	/// </summary>
	public class ObjectLocater<T> : MonoBehaviour where T : StageObject {

		[SerializeField]
		private T[] objects;
		[Header("配置オプション")]
		[SerializeField, MinMaxRange(0, 20)]
		private MinMax locateNum;
		[SerializeField, MinMaxRange(0, 500)]
		private MinMax locateDensity;

		#region UnityEvent

		private void Awake() {
			//テスト配置
			Locate(locateNum.randomInt, locateDensity.random);
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
				var obj = Instantiate(objects[Random.Range(0, objects.Length)]);
				//取り敢えず親
				obj.transform.SetParent(transform);
				obj.transform.position = pos;
				//取り敢えず初期化
				obj.InitPoolable();
			}
		}

		#endregion
	}
}