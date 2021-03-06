﻿using UnityEngine;
using System;
using System.Collections.Generic;

namespace MassLine {
	
	/// <summary>
	/// 色の更新器
	/// </summary>
	public class GradientUpdater : LineUpdater {
		 
		private Gradient gradient;

		public GradientUpdater(Gradient gradient) {
			this.gradient = gradient;
		}

		#region VirtualFunction

		/// <summary>
		/// 更新
		/// </summary>
		public override void Update() {
			float lifeTime = line.LifeTime;
			foreach (var v in line.Vertices) {
				v.color = gradient.Evaluate(v.lifeTime / lifeTime);
			}
		}

		#endregion
	}
}