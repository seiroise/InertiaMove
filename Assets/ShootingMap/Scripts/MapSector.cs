using UnityEngine;
using System;

namespace ShootingMap {

	/// <summary>
	/// 地図セクター
	/// </summary>
	[Serializable]
	public class MapSector {
		
		[SerializeField]
		private string description;
		[SerializeField]
		private MapNode[] nodes;

		#region Function

		/// <summary>
		/// ノードの配置
		/// </summary>
		public void LocateNodes(float width, GameObject nodePrefab) {
			float deltaWidth = width / nodes.Length;
			for(int i = 0; i < nodes.Length; ++i) {
				
			}
		}

		#endregion
	}
}