using UnityEngine;
using System;
using System.Collections.Generic;
using InertiaShooting.Weapon.Bullet;

namespace InertiaShooting.Weapon {

	/// <summary>
	/// 装備の操作
	/// </summary>
	public class WeaponController : MonoBehaviour {

		[Header("発射関連")]
		[SerializeField, Range(0.01f, 60f)]
		private float interval = 0.3f;
		private float measureInterval = 0f;

		[Header("弾関連")]
		[SerializeField]
		private BulletPool bulletSource;
		[SerializeField]
		private Transform shotPos;

		[Header("回転関連")]
		[SerializeField]
		private Rotation2D axis;

		[Header("アニメーション関連")]
		[SerializeField]
		private Animator animator;
		[SerializeField]
		private string activateAnim;
		[SerializeField]
		private string disactivateAnim;
		[SerializeField]
		private string shotAnim = "Fire";

		//その他
		private bool active = false;

		#region UnityEvent

		private void Update() {
			if (active) {
				measureInterval += Time.deltaTime;
				if (measureInterval > interval) {
					Fire();
					measureInterval = 0f;
				}
			}
		}

		#endregion

		#region Function

		/// <summary>
		/// 発射
		/// </summary>
		public void Fire() {
			//弾の生成
			InstantiateBullet();
			//発射アニメーション
			RunAnimation(shotAnim);
		}

		/// <summary>
		/// 弾の生成
		/// </summary>
		private void InstantiateBullet() {
			if (bulletSource) {
				BulletController b = bulletSource.GetObject();
				b.transform.position = shotPos.position;
				b.transform.eulerAngles = shotPos.eulerAngles;
			}
		}

		/// <summary>
		/// アニメーションの再生
		/// </summary>
		private void RunAnimation(string name) {
			if (animator) {
				animator.SetTrigger(name);
			}
		}

		/// <summary>
		/// 発射角の設定
		/// </summary>
		public void SetAngle(float angle) {
			axis.SetAngle(angle);
		}

		/// <summary>
		/// 有効化
		/// </summary>
		public void Activate() {
			axis.IsLocal = false;
			active = true;
		}

		/// <summary>
		/// 無効化
		/// </summary>
		public void Disactivate() {
			axis.IsLocal = true;
			axis.SetAngle(0f);
			active = false;
		}

		#endregion

	}
}