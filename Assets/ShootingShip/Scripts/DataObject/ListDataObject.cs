using UnityEngine;
using System.Collections.Generic;
using ShootingShip.Structure;

namespace ShootingShip.DataObject {

	/// <summary>
	/// リストのデータオブジェクト
	/// </summary>
	public class ListDataObject<T> : ScriptableObject {

		public List<T> datas;

		#region Function

		/// <summary>
		/// ランダムに取得
		/// </summary>
		public T Get() {
			return datas[Random.Range(0, datas.Count)];
		}

		#endregion
	}
}