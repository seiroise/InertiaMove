using UnityEngine;
using System.Collections;
using ShootingUtility.ObjectPool;

namespace ShootingUtility.Particle {

	/// <summary>
	/// オブジェクトプール用のパーティクル
	/// </summary>
	public class ObjectPoolParticle : MonoBehaviour, IPoolable {

		private ParticleSystem ps;
		public Vector3 defaultAngles;

		#region UnityEvent

		private void Update() {
			if (!ps.IsAlive(true)) {
				gameObject.SetActive(false);
			}
		}

		#endregion

		#region IPoolable

		/// <summary>
		/// 初期化
		/// </summary>
		public void InitPoolable() {
			if (!ps) ps = GetComponent<ParticleSystem>();
			transform.eulerAngles = defaultAngles;
			if (ps) {
				ps.Play(true);
			}
		}

		#endregion
	}
} //namespace end