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
			
			private Com _com;
			public Com com { get { return _com; } }
			private Type _comType;
			public Type comType { get { return comType; } }

			/// <summary>
			/// コンストラクタ
			/// </summary>
			public ComTag(Com com) {
				this._com = com;
				this._comType = com.GetType();
			}


			public static bool operator true(ComTag c) {
				return c != null;
			}
			public static bool operator false(ComTag c) {
				return c == null;
			}

		}

		[SerializeField, Button("SetChildrenCom", "SetChildrenCom")]
		private int _btn1;

		[SerializeField]
		private Com[] _initComs;       //初期化時登録コンポーネント

		protected List<ComTag> _comList;   //登録コンポーネント
		public int comCount { get { return _comList != null ? _comList.Count : 0; } }

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			_comList = new List<ComTag>();
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
			foreach(var c in _initComs) {
				if(c) {
					c.STGInit(manager);
					_comList.Add(new ComTag(c));
				}
			}
		}

		/// <summary>
		/// 登録コンポーネントの起動
		/// </summary>
		private void AwakeComs() {
			foreach(var c in _comList) {
				if(c) c.com.STGAwake();
			}
		}

		/// <summary>
		/// コンポーネントの追加
		/// </summary>
		public void AddCom(Com com) {
			if(com && _comList != null) {
				_comList.Add(new ComTag(com));
				com.STGInit(manager);
				com.STGAwake();
			}
		}

		/// <summary>
		/// コンポーネントの削除
		/// </summary>
		public void RemoveCom(Com com) {
			if(com && _comList != null) {
				for(int i = 0; i < _comList.Count; ++i) {
					if(_comList[i].com == com) {
						Destroy(_comList[i].com);
						_comList.RemoveAt(i);
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
			for(int i = 0; i < _comList.Count; ++i) {
				if(_comList[i].comType == type) {
					return (T)_comList[i].com;
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
			_initComs = coms.ToArray();
		}

		#endregion
	}
}