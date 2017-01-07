using UnityEngine;
using System;

namespace ShootingMap {

	/// <summary>
	/// 地図ノード
	/// </summary>
	[Serializable]
	public class MapNode {

		[SerializeField]
		private string description;
		[SerializeField]
		private Color color;
	}
}