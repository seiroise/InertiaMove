using UnityEngine;
using System;
using System.Collections.Generic;
using STG.BaseUtility.ObjectPool;

namespace STG.Utility.BGScroll {

	/// <summary>
	/// 背景オブジェクト
	/// </summary>
	[AddComponentMenu("STG/Utility/BGScroll/BGObject")]
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	public class BGObject : MonoBehaviour, IPoolable {

		#region IPoolable

		/// <summary>
		/// 初期化
		/// </summary>
		public void InitPoolable() { }

		#endregion
	}
}