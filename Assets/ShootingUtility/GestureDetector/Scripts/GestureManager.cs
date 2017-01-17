using UnityEngine;
using System;
using ShootingShip.Utility;

namespace ShootingUtility.GestureDetector {

	/// <summary>
	/// ジェスチャー入力の管理
	/// </summary>
	public class GestureManager : SingletonMonoBehaviour<GestureManager> {

		[SerializeField]
		private SwipeDetector swipe;
		public SwipeDetector Swipe { get { return swipe; } }
		[SerializeField]
		private FlickDetector flick;
		public FlickDetector Flick { get { return flick; } }
	}
}