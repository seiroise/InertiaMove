using UnityEngine;
using System;

namespace STG.BaseUtility.ComSystem {

	/// <summary>
	/// STG用のコンポーネント管理者
	/// </summary>
	public class STGComManager : STGAbstractComManager<STGCom> {

		[SerializeField]
		private bool autoInit;

		#region UnityEvent

		protected virtual void Awake() {
			if(autoInit) {
				STGInit(this);
				STGAwake();
			}
		}

		#endregion

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void STGInit(STGComManager manager) {
			//管理者を中継しない
			base.STGInit(this);
			//でもこいつの親は管理者
			this.manager = manager;
		}

		#endregion
	}
}