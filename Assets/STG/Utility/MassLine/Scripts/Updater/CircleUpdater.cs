using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STG.Utility.MassLine {

	/// <summary>
	/// 円形に広がる
	/// </summary>
	public class CircleUpdater : LineUpdater {

		private Vector3 center;
		private float speed;
		private int vertNum;
		private Vector3[] moves;
		private int index;

		public CircleUpdater(Vector3 center, float speed, int vertNum) {
			this.center = center;
			this.speed = speed;
			this.vertNum = vertNum += 1;
			//予め移動量を求めておく
			if (vertNum > 2) {
				this.moves = new Vector3[vertNum];
				float deltaAngle = 360f / (vertNum - 1);
				float rad = 0f;
				for (int i = 0; i < vertNum; ++i) {
					rad = Mathf.Deg2Rad * (deltaAngle * i);
					moves[i] = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
				}
			}
		}

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void Init(Line line) {
			base.Init(line);
			//指定分の頂点を追加
			if (vertNum > 2) {
				for (int i = 0; i < vertNum; ++i) {
					line.AddVertex(center);
				}
			}
		}

		/// <summary>
		/// 更新
		/// </summary>
		public override void Update() {
			if (!enable) return;
			index = 0;
			foreach(var v in line.Vertices) {
				v.position += moves[index] * Time.deltaTime * speed;
				++index;
			}
		}

		#endregion
	}
}