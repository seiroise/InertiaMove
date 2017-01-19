using UnityEngine;
using ShootingUtility.ComSystem;
using ShootingUtility.ObjectDetector;

namespace STG.Obj.Marker {

	/// <summary>
	/// STGオブジェクト用のマーカー
	/// </summary>
	[RequireComponent(typeof(DetectableRigidbody2D))]
	public class STGObjMarker : STGCom {

		[SerializeField]
		private SpriteRenderer markerRenderer;
		public SpriteRenderer MarkerRenderer { get { return markerRenderer; } }

		private DetectableRigidbody2D detectable;
		public DetectableRigidbody2D Detectable { get { return detectable; } }

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			detectable = GetComponent<DetectableRigidbody2D>();
		}

		#endregion
	}
}