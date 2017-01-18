using UnityEngine;
using System;
using System.Collections.Generic;
using STG.BaseUtility.ObjectPool;

namespace STG.Utility.BGScroll {

	/// <summary>
	/// 背景オブジェクトプール
	/// </summary>
	[AddComponentMenu("STG/Utility/BGScroll/BGPool")]
	public class BGPool : AbstractObjectPool<BGObject> { }
}