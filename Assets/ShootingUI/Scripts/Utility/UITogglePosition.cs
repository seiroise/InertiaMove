using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ShootingUI.Utility {

	/// <summary>
	/// スイッチの押下で座標を変更
	/// </summary>
	[RequireComponent(typeof(Button))]
	public class UITogglePosition : MonoBehaviour {

		[Header("座標")]
		[SerializeField]
		private RectTransform targetRect;
		[SerializeField]
		private Vector3 posA;
		[SerializeField]
		private Vector3 posB;

		private bool isA = true;

		#region UnityEvent

		private void Awake() {
			var btn = GetComponent<Button>();
			btn.onClick.RemoveListener(OnClick);
			btn.onClick.AddListener(OnClick);
		}

		#endregion

		#region Function

		/// <summary>
		/// 座標のスイッチ
		/// </summary>
		private void SwitchPosition() {
			if (targetRect) {
				targetRect.localPosition = isA ? posB : posA;
				isA = !isA;
			}
		}

		#endregion

		#region Callback

		/// <summary>
		/// クリック
		/// </summary>
		private void OnClick() {
			SwitchPosition();
		}

		#endregion
	}
}