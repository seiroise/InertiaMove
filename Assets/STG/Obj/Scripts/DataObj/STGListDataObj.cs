using UnityEngine;
using System.Collections.Generic;

namespace STG.Obj.DataObj {

	/// <summary>
	/// STG用のリストデータオブジェクト
	/// </summary>
	public class STGListDataObj<T> : STGDataObj where T : Component {
		
		[SerializeField]
		private T[] datas;

		private Dictionary<string, T> dataDic;

		#region Function

		/// <summary>
		/// ランダムに取得
		/// </summary>
		public T Get() {
			return datas[Random.Range(0, datas.Length)];
		}

		/// <summary>
		/// 名前を指定して取得
		/// </summary>
		public T Get(string name) {
			if (dataDic == null) {
				dataDic = new Dictionary<string, T>();
				foreach (var t in datas) {
					if(!dataDic.ContainsKey(t.name)) {
						dataDic.Add(t.name, t);
					}
				}
			}
			return dataDic.ContainsKey(name) ? dataDic[name] : null;
		}

		#endregion
	}
}