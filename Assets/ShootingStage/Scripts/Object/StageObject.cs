using UnityEngine;
using System.Collections;
using ShootingUtility.ObjectPool;

namespace ShootingStage.Object {

	/// <summary>
	/// ステージ上のオブジェクトの元
	/// </summary>
	public class StageObject : MonoBehaviour, IPoolable {

		#region IPoolable

		public void InitPoolable() {
			
		}

		#endregion
	}
}