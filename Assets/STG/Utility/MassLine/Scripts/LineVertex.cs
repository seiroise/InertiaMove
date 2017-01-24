using UnityEngine;
using System;
using System.Collections.Generic;

namespace STG.Utility.MassLine {

	/// <summary>
	/// 線の頂点
	/// </summary>
	[Serializable]
	public class LineVertex {
		public Vector3 position;	//座標
		public Color color;			//色
		public float lifeTime;		//生存時間

		public LineVertex(Vector3 position, Color color) {
			this.position = position;
			this.color = color;
		}

		public LineVertex(Vector3 position)
			: this(position, Color.white) {
		}
	}
}