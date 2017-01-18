using System;
using System.Collections.Generic;

namespace STG.BaseUtility.ObjectPool {

	/// <summary>
	/// プール可能
	/// </summary>
	public interface IPoolable {

		/// <summary>
		/// 初期化
		/// </summary>
		void InitPoolable();
	}
}