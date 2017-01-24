using UnityEngine;
using System;
using System.Collections.Generic;
using EditorUtility;

namespace STG.BaseUtility.ComSystem {

	/// <summary>
	/// STG用のコンポーネントを複数持つ抽象基底オブジェクト
	/// </summary>
	public abstract class STGAbstractComManager<Com> : STGCom where Com : STGCom {

		/// <summary>
		/// 登録されているコンポーネントの識別タグ
		/// </summary>
		protected class ComTag {
			public Com com;
			public Type comType;

			/// <summary>
			/// コンストラクタ
			/// </summary>
			public ComTag(Com com) {
				this.com = com;
				this.comType = com.GetType();
			}


			public static bool operator true(ComTag c) {
				return c != null;
			}
			public static bool operator false(ComTag c) {
				return c == null;
			}

		}

		[SerializeField, Button("SetChildrenCom", "SetChildrenCom")]
		private int btn1;

		[SerializeField]
		private Com[] initComs;       //初期化時登録コンポーネント

		protected List<ComTag> comList;   //登録コンポーネント
		public int comCount { get { return comList != null ? comList.Count : 0; } }

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			comList = new List<ComTag>();
			InitComs(manager);
		}

		/// <summary>
		/// 起動
		/// </summary>
		public override void STGAwake() {
			base.STGAwake();
			AwakeComs();
		}

		#endregion

		#region Function

		/// <summary>
		/// 登録コンポーネントの初期化
		/// </summary>
		private void InitComs(STGComManager manager) {
			foreach(var c in initComs) {
				if(c) {
					c.STGInit(manager);
					comList.Add(new ComTag(c));
				}
			}
		}

		/// <summary>
		/// 登録コンポーネントの起動
		/// </summary>
		private void AwakeComs() {
			foreach(var c in comList) {
				if(c) c.com.STGAwake();
			}
		}

		/// <summary>
		/// コンポーネントの追加
		/// </summary>
		public void AddCom(Com com) {
			if(com && comList != null) {
				comList.Add(new ComTag(com));
				com.STGInit(manager);
				com.STGAwake();
			}
		}

		/// <summary>
		/// コンポーネントの削除
		/// </summary>
		public void RemoveCom(Com com) {
			if(com && comList != null) {
				for(int i = 0; i < comList.Count; ++i) {
					if(comList[i].com == com) {
						Destroy(comList[i].com);
						comList.RemoveAt(i);
						return;
					}
				}
			}
		}

		/// <summary>
		/// コンポーネントの取得
		/// </summary>
		public T GetCom<T>() where T : Com {
			Type type = typeof(T);
			for(int i = 0; i < comList.Count; ++i) {
				if(comList[i].comType == type) {
					return (T)comList[i].com;
				}
			}
			return null;
		}

		#endregion

		#region ButtonFunction

		/// <summary>
		/// 子(距離1)の持っているComを初期登録コンポーネントに設定する
		/// </summary>
		public void SetChildrenCom() {
			List<Com> coms = new List<Com>();
			foreach(Transform t in transform) {
				Com[] c;
				if((c = t.GetComponents<Com>()) != null) {
					coms.AddRange(c);
				}
			}
			initComs = coms.ToArray();
		}

		#endregion
	}
}