using UnityEngine;
using System;
using System.Collections.Generic;

namespace STG.Utility.MassLine {
	
	/// <summary>
	/// 納豆っぽい糸のエフェクト用
	/// </summary>
	public class NattouTrailUpdater : LineUpdater {

		private float lifeTime;
		private Vector3 position;
		private Vector3 velocity;
		private Vector3 gravity;

		public NattouTrailUpdater(float lifeTime, Vector3 position, Vector3 velocity, Vector3 gravity) {
			this.lifeTime = lifeTime;
			this.position = position;
			this.velocity = velocity;
			this.gravity = gravity;
		}

		#region VirtualFunction

		public override void Init(Line line) {
			base.Init(line);
			this.line.AddVertex(position);
		}

		public override void Update() {
			if (!enable) return;
			if(lifeTime > 0f) {
				lifeTime -= Time.deltaTime;
				velocity -= gravity * Time.deltaTime;
				position += velocity * Time.deltaTime;
				this.line.AddVertex(position);
			}
		}

		#endregion
	}
}