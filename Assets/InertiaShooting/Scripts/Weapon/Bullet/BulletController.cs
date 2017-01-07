using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingUtility.ObjectPool;

namespace InertiaShooting.Weapon.Bullet {

	/// <summary>
	/// 弾
	/// </summary>
	public class BulletController : MonoBehaviour, IPoolable {

		[SerializeField, Range(0f, 100f)]
		private float speed = 10f;

		[SerializeField, Range(0f, 120f)]
		private float lifeTime = 5f;
		private float measureLifeTime;

		#region UnityEvent

		private void Update() {
			transform.Translate(Vector2.right * speed * Time.deltaTime);
			measureLifeTime += Time.deltaTime;
			if (measureLifeTime > lifeTime) {
				gameObject.SetActive(false);
			}
		}

		#endregion

		#region IPoolable

		/// <summary>
		/// 初期化
		/// </summary>
		public void InitPoolable() {
			measureLifeTime = 0f;
		}

		#endregion
	}
}