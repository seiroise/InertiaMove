using UnityEngine;
using System;

namespace ShootingMap {

	/// <summary>
	/// 地図の管理
	/// </summary>
	public class MapManager : MonoBehaviour {

		/// <summary>
		/// セクタ
		/// </summary>
		[Serializable]
		public class Sector {
			[SerializeField]
			private string description;
			[SerializeField]
			private Node[] nodes;

			#region Function

			/// <summary>
			/// ノードの配置
			/// </summary>
			public void LocateNodes(Transform parent, GameObject nodePrefab, float widthInterval) {
				float x = -(widthInterval / 2) * (nodes.Length - 1);
				for (int i = 0; i < nodes.Length; ++i) {
					var obj = Instantiate(nodePrefab);
					obj.name = string.Format("Node {0:D2}", i);
					obj.transform.SetParent(parent);
					obj.transform.localPosition = new Vector3(x + widthInterval * i, 0f, 0f);
				}
			}

			#endregion
		}

		/// <summary>
		/// ノード
		/// </summary>
		[Serializable]
		public class Node {
			[SerializeField]
			private string description;
			[SerializeField]
			private Color color;
		}

		[SerializeField]
		private GameObject nodePrefab;
		[SerializeField]
		private Sector[] sectors;
		[SerializeField, Range(0.1f, 2f)]
		private float sectorInterval = 1f;
		[SerializeField, Range(0.1f, 2f)]
		private float widthInterval = 0.5f;

		#region Function

		/// <summary>
		/// ノードの配置
		/// </summary>
		[ContextMenu("Locate Nodes")]
		public void LocateNodes() {
			LocateNodes(sectorInterval, widthInterval);
		}

		/// <summary>
		/// ノードの配置
		/// </summary>
		public void LocateNodes(float sectorInterval, float widthInterval) {
			for (int i = 0; i < sectors.Length; ++i) {
				var t = new GameObject().transform;
				t.name = string.Format("Sector {0:D2}", i);
				t.SetParent(transform);
				t.localPosition = new Vector3(0f, sectorInterval * i, 0f);
				sectors[i].LocateNodes(t, nodePrefab, widthInterval);
			}
		}

		#endregion
	}
}