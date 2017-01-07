using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ShootingUtility.ObjectPool;

namespace ShootingUtility.BackgroundScroll {

	/// <summary>
	/// 背景オブジェクトのプール
	/// </summary>
	public class BackgroundPool : AbstractObjectPool<TrackingUVScroll> {}
}