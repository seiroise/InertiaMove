using UnityEngine;
using System;
using System.Collections.Generic;

namespace STG.Utility.MassLine {

	/// <summary>
	/// 追尾線の更新器
	/// </summary>
	public class TrailUpdater : LineUpdater {

		private Transform trans;
		private Vector3 prevPos;
		private float distanceRatio;

		public TrailUpdater(Transform trans, float distanceRatio) {
			this.trans = trans;
			this.distanceRatio = distanceRatio;
			this.prevPos = trans.position;
		}

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void Init(Line line) {
			base.Init(line);
			this.line.AddVertex(prevPos);
		}

		/// <summary>
		/// 更新
		/// </summary>
		public override void Update() {
			if (!enable) return;
			if (trans) {
				float distance = Vector3.Distance(trans.position, prevPos);
				if (distance > distanceRatio) {
					prevPos = trans.position;
					line.AddVertex(prevPos);
				} else {
					LineVertex vert = line.GetLast();
					if (vert != null) vert.position = trans.position;
				}
			}
		}

		#endregion
	}
}