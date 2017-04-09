using UnityEngine;
using System;

namespace STG.Obj.DataObj {

	/// <summary>
	/// STG用のデータオブジェクト
	/// </summary>
	public class STGDataObj : ScriptableObject {

		[SerializeField]
		private string _nickname;
		public string nickname { get { return _nickname; } }
		[SerializeField]
		private string _description;
		public string description { get { return _description; } }
	}
}