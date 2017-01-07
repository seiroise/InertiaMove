using UnityEngine;
using System;

namespace ShootingUtility.ObjectPool {

	/// <summary>
	/// オブジェクトプールに適応可能
	/// </summary>
	public interface IPoolable {

		/// <summary>
		/// オブジェクトの初期化
		/// </summary>
		void InitPoolable();
	}
}