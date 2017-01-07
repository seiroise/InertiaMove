using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MassLine {

	/// <summary>
	/// 線の更新用インタフェース
	/// </summary>
	public abstract class LineUpdater {

		protected LineEffect line;

		protected bool enable;
		public bool Enable { get { return enable; } set { enable = value; } }

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public virtual void Init(LineEffect line) {
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