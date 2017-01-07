using UnityEngine;
using System;
using System.Collections;
using ShootingUtility.ObjectPool;
using ShootingUtility.ObjectDetector;

namespace InertiaShooting {

	/// <summary>
	/// キャラクター
	/// </summary>
	public class Character : MonoBehaviour, IPoolable {

		[SerializeField]
		private CharacterParameter baseStatus;
		private CharacterParameter nowStatus;

		#region IPoolable

		/// <summary>
		/// 初期化
		/// </summary>
		public virtual void InitPoolable() {
			//ステータスの初期化
			nowStatus = baseStatus;
		}

		#endregion
	}
}