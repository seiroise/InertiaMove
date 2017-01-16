using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ShootingSearch.Test {

	/// <summary>
	/// searchモードのテスト
	/// </summary>
	[RequireComponent(typeof(Button))]
	public class TestSearchMode : MonoBehaviour {

		[Header("UI")]
		[SerializeField]
		private Text indicateText;

		private bool isSearched;

		#region UnityEvent

		private void Awake() {
			var btn = GetComponent<Button>();
			if(btn) {
				btn.onClick.RemoveListener(OnClick);
				btn.onClick.AddListener(OnClick);
			}
		}

		private void Start() {
			ExitSearchMode();
		}

		#endregion

		#region Function

		/// <summary>
		/// サーチモードの切り替え
		/// </summary>
		public void ToggleSearchMode() {
			if (isSearched) {
				ExitSearchMode();
			} else {
				EnterSearchMode();
			}
			isSearched = !isSearched;
		}

		/// <summary>
		/// searchモードの開始
		/// </summary>
		private void EnterSearchMode() {
			Time.timeScale = 0f;
			if (indicateText) {
				indicateText.enabled = true;
			}
		}

		/// <summary>
		/// searchモードの終了
		/// </summary>
		public void ExitSearchMode() {
			Time.timeScale = 1f;
			if (indicateText) {
				indicateText.enabled = false;
			}
		}

		#endregion

		#region Callback

		/// <summary>
		/// ボタン押下
		/// </summary>
		private void OnClick() {
			ToggleSearchMode();
		}

		#endregion
	}
}