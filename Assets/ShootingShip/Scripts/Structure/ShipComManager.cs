using UnityEngine;
using System;
using System.Collections.Generic;

namespace ShootingShip.Structure {
	
	/// <summary>
	/// 機体の部品を管理する部品
	/// </summary>
	public abstract class ShipComManager<T> : ShipCom where T : ShipCom {

		[SerializeField]
		protected T[] coms;
		public T[] Coms { get { return coms; } }
		protected Dictionary<string, T> comDic;

		#region VirtualFunction

		/// <summary>
		/// 部品の初期化
		/// </summary>
		public override void InitCom(ShipStructure structure) {
			base.InitCom(structure);
			InitComs(structure, coms);
		}

		/// <summary>
		/// 部品の起動
		/// </summary>
		public override void AwakeCom() {
			base.AwakeCom();
			foreach(var com in coms) {
				com.AwakeCom();
			}
		}

		#endregion

		#region Function

		/// <summary>
		/// 登録されている部品の初期化
		/// </summary>
		private void InitComs(ShipStructure structure, T[] coms) {
			comDic = new Dictionary<string, T>();
			foreach (var com in coms) {
				if (comDic.ContainsKey(com.name)) continue;
				comDic.Add(com.name, com);
			}
			foreach (var com in coms) {
				com.InitCom(structure);
			}
		}

		/// <summary>
		/// 登録されている部品の取得
		/// </summary>
		public T GetCom(string name) {
			if (comDic.ContainsKey(name)) return comDic[name];
			return null;
		}

		/// <summary>
		/// 登録されている部品の数を取得
		/// </summary>
		public int ComsCount() {
			return coms.Length;
		}

		#endregion
	}
}