using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ShootingSearch.Test {

	/// <summary>
	/// searchモードのテスト
	/// </summary>
	[RequireComponent(typeof(Button))]
	public class TestSearchMode : MonoBehaviour {

		private bool isSearched;

		#region UnityEvent

		private void Awake() {
			var btn = GetComponent<Button>();
			if(btn) {
				btn.onClick.RemoveListener(OnClick);
				btn.onClick.AddListener(OnClick);
			}
		}

		#endregion

		#region Function

		/// <summary>
		/// searchモードの開始
		/// </summary>
		public int EnterSearchMode() {
			return 0;
		}

		/// <summary>
		/// searchモードの終了
		/// </summary>
		public int ExitSearchMode() {
			return 0;
		}

		#endregion

		#region Callback

		/// <summary>
		/// ボタン押下
		/// </summary>
		private void OnClick() {
			//とりあえず時間を止めてみる
			isSearched == true ? EnterSearchMode() : ExitSearchMode();
		}

		#endregion
	}
}