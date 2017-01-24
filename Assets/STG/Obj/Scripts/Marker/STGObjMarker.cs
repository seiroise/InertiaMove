using UnityEngine;
using ShootingUtility.ObjectDetector;
using STG.Obj.Targeting;
using STG.BaseUtility.ComSystem;

namespace STG.Obj.Marker {

	/// <summary>
	/// STGオブジェクト用のマーカー
	/// </summary>
	[RequireComponent(typeof(DetectableSTGObj))]
	public class STGObjMarker : STGCom {

		[SerializeField]
		private SpriteRenderer markerRenderer;
		public SpriteRenderer MarkerRenderer { get { return markerRenderer; } }

		private DetectableSTGObj detectable;
		public DetectableSTGObj Detectable { get { return detectable; } }

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			detectable = GetComponent<DetectableSTGObj>();
		}

		#endregion
	}
}