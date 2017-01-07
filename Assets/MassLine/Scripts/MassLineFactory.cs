using UnityEngine;
using System;
using System.Collections.Generic;

namespace MassLine {

	/// <summary>
	/// 大量の線の工場
	/// </summary>
	public class MassLineFactory : MonoBehaviour {

		[SerializeField]
		private Material material;
		[SerializeField]
		private List<LineEffect> lines;

		private Mesh mesh;
		private List<LineVertex> lineVerts;
		private List<Vector3> vertices;
		private List<Color> colors;
		private List<int> indices;

		#region UnityEvent

		private void Awake() {
			lines = new List<LineEffect>();
			mesh = new Mesh();
			lineVerts = new List<LineVertex>();
			vertices = new List<Vector3>();
			colors = new List<Color>();
			indices = new List<int>();
		}

		private void Update() {
			DrawLines();
		}

		#endregion

		#region Function

		/// <summary>
		/// 線群を描画する
		/// </summary>
		private void DrawLines() {
			if (lines == null || lines.Count <= 0) return;

			mesh.Clear();
			vertices.Clear();
			colors.Clear();
			indices.Clear();

			int i, j;

			//更新と描画
			for (i = lines.Count - 1; i >= 0; --i) {
				LineEffect l = lines[i];
				lineVerts = l.Update();
				//削除確認
				if (l.IsDead) {
					lines.RemoveAt(i);
					continue;
				}
				if (lineVerts.Count > 1) {
					for (j = 0; j < lineVerts.Count - 1; ++j) {
						//頂点
						vertices.Add(lineVerts[j].position);
						//頂点カラー
						colors.Add(lineVerts[j].color);
						//インデックス
						indices.Add(vertices.Count - 1);
						indices.Add(vertices.Count);
					}
					//頂点
					vertices.Add(lineVerts[j].position);
					//頂点カラー
					colors.Add(lineVerts[j].color);
				}
			}
			mesh.SetVertices(vertices);
			mesh.SetColors(colors);
			mesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
			Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
		}

		/// <summary>
		/// 線を作成
		/// </summary>
		public LineEffect CreateLine(Color color, float lifeTime) {
			LineEffect line = new LineEffect(color, lifeTime);
			lines.Add(line);
			return line;
		}

		/// <summary>
		/// 線を作成
		/// </summary>
		public LineEffect CreateLine(Color color, float lifeTime, params LineUpdater[] updaters) {
			LineEffect line = new LineEffect(color, lifeTime, updaters);
			lines.Add(line);
			return line;
		}

		/// <summary>
		/// 線の削除
		/// </summary>
		public void DeleteLine(LineEffect line) {
			lines.Remove(line);
		}

		#endregion
	}
}