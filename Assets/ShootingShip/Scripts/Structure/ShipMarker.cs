using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingUtility.ObjectDetector;

namespace ShootingShip.Structure {

	/// <summary>
	/// 機体のマーカー
	/// </summary>
	[RequireComponent(typeof(DetectableObject2D))]
	public class ShipMarker : ShipCom {

		[SerializeField]
		private SpriteRenderer marker;
		public SpriteRenderer Marker { get { return marker; } }

		[SerializeField]
		private ObjectDetector2D objDetector;
		public ObjectDetector2D ObjDetector { get { return objDetector; } }

		private DetectableObject2D detectableObj;
		public DetectableObject2D DetectableObj { get { return detectableObj; } }
		
		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(ShipStructure structure) {
			base.InitCom(structure);
			structure.Marker = this;
			detectableObj = GetComponent<DetectableObject2D>();
		}

		#endregion
	}
}