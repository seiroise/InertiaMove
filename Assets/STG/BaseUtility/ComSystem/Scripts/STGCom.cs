using UnityEngine;
using System;

namespace STG.BaseUtility.ComSystem {

	/// <summary>
	/// STG用のコンポーネント
	/// </summary>
	public class STGCom : MonoBehaviour {

		protected STGComManager manager;
		public STGComManager Manager { get { return manager; } }

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public virtual void STGInit(STGComManager manager) {
			this.manager = manager;
		}

		/// <summary>
		/// 起動
		/// </summary>
		public virtual void STGAwake() { }

		#endregion
	}
}