using UnityEngine;
using System;
using System.Collections.Generic;

namespace STG.Utility.MassLine {

	/// <summary>
	/// 線
	/// </summary>
	[Serializable]
	public class Line {

		[SerializeField]
		private List<LineVertex> vertices;
		public List<LineVertex> Vertices { get { return vertices; } }
		private Color baseColor;
		public Color BaseColor { get { return baseColor; } }
		private float lifeTime;
		public float LifeTime { get { return lifeTime; } }
		private LineUpdater[] updaters;

		private bool isAutoDead;
		public bool IsAutoDead { get { return isAutoDead; } set { isAutoDead = value; } }

		private bool isDead;
		public bool IsDead { get { return isDead; } set { isDead = value; } }

		#region Constructors

		public Line(Color baseColor, float lifeTime, params LineUpdater[] updaters) {
			this.vertices = new List<LineVertex>();
			this.baseColor = baseColor;
			this.lifeTime = lifeTime;
			this.updaters = updaters;
			this.isDead = false;

			foreach (var u in updaters) {
				u.Init(this);
			}
		}

		#endregion

		#region Function

		/// <summary>
		/// 更新
		/// </summary>
		public List<LineVertex> Update() {
			foreach (var u in updaters) {
				u.Update();
			}
			//時間更新
			UpdateTime();
			//死亡判定
			if (isAutoDead) {
				if (vertices.Count <= 0) {
					isDead = true;
				}
			}
			return vertices;
		}

		/// <summary>
		/// 時間の更新
		/// </summary>
		public void UpdateTime() {
			for (int i = vertices.Count - 1; i >= 0; --i) {
				vertices[i].lifeTime += Time.deltaTime;
				//削除
				if (vertices[i].lifeTime > lifeTime) {
					vertices.RemoveAt(i);
				}
			}
		}

		/// <summary>
		/// 頂点の追加
		/// </summary>
		public void AddVertex(Vector3 position) {
			vertices.Add(new LineVertex(position, baseColor));
		}

		/// <summary>
		/// 頂点の追加
		/// </summary>
		public void AddVertices(List<Vector3> positions) {
			foreach (var p in positions) {
				vertices.Add(new LineVertex(p, baseColor));
			}
		}

		/// <summary>
		/// 末尾の頂点を取得
		/// </summary>
		public LineVertex GetLast() {
			if (vertices.Count <= 0) return null;
			return vertices[vertices.Count - 1];
		}

		#endregion
	}
}