using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ShootingUtility.UIViewport {

	/// <summary>
	/// UGUIで指定した矩形に対してカメラの投影を行う
	/// Canvas.renderModeのWorldSpaceには非対応
	/// </summary>
	[ExecuteInEditMode]
	public class UIViewportCamera : MonoBehaviour {

		[SerializeField]
		private Camera cam;

		[SerializeField]
		public RectTransform rootRect;

		[SerializeField]
		public RectTransform rect;

		#region UnityEvent

		private void OnEnable() {
			UpdateViewport();
		}

		#endregion

		#region Function

		/// <summary>
		/// RectTransformとそのCanvasの大きさからViewport座標を求める
		/// </summary>
		private Rect GetViewportRect(RectTransform rootRect, RectTransform rect) {
			//4隅の絶対座標を求める
			Vector3[] rootCorner = new Vector3[4];
			Vector3[] corner = new Vector3[4];
			rootRect.GetWorldCorners(rootCorner);
			rect.GetWorldCorners(corner);

			//viewport座標に変換
			Vector2 rootDelta = rootCorner[2] - rootCorner[0];
			Vector2 bottomLeft = corner[0] - rootCorner[0];
			bottomLeft.x /= rootDelta.x;
			bottomLeft.y /= rootDelta.y;
			Vector2 delta = corner[2] - corner[0];
			delta.x /= rootDelta.x;
			delta.y /= rootDelta.y;

			return new Rect(
				bottomLeft.x,
				bottomLeft.y,
				delta.x,
				delta.y);
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void UpdateViewport() {
			if(cam && rootRect && rect) {
				cam.rect = GetViewportRect(rootRect, rect);
			}
		}

		#endregion

	}
}