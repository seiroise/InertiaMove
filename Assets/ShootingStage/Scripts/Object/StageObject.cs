using UnityEngine;
using System.Collections;
using ShootingUtility.ObjectPool;

namespace ShootingStage.Object {

	/// <summary>
	/// ステージ上のオブジェクト
	/// </summary>
	public abstract class StageObject : MonoBehaviour, IPoolable {

		#region IPoolable

		public virtual void InitPoolable() {
			
		}

		#endregion
	}
}