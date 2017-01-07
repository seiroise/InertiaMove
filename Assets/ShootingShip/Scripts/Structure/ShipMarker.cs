using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingUtility.ObjectDetector;

namespace ShootingShip.Structure {

	/// <summary>
	/// 機体のマーカー
	/// </summary>
	[RequireComponent(typeof(RigidbodyDetector2D))]
	public class ShipMarker : ShipCom {

		[SerializeField]
		private SpriteRenderer marker;
		public SpriteRenderer Marker { get { return marker; } }

		private RigidbodyDetector2D detector;
		public RigidbodyDetector2D Detector { get { return detector; } }

		[SerializeField]
		private DetectableRigidbody2D detectable;
		public DetectableRigidbody2D Detectable { get { return detectable; } }
		
		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(ShipStructure structure) {
			base.InitCom(structure);
			structure.Marker = this;
			detector = GetComponent<RigidbodyDetector2D>();
		}

		#endregion
	}
}