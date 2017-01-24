using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STG.Utility.MassLine {

	/// <summary>
	/// 線の更新用インタフェース
	/// </summary>
	public abstract class LineUpdater {

		protected Line line;

		protected bool enable;
		public bool Enable { get { return enable; } set { enable = value; } }

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public virtual void Init(Line line) {
			this.line = line;
			this.enable = true;
		}

		/// <summary>
		/// 更新
		/// </summary>
		public abstract void Update();

		#endregion
	}
}