using UnityEngine;
using System;
using System.Collections.Generic;

namespace STG.BaseUtility.ObjectPool {

	/// <summary>
	/// オブジェクトプール
	/// </summary>
	public abstract class AbstractObjectPool<T> : MonoBehaviour where T : Component , IPoolable {

		/// <summary>
		/// プール
		/// </summary>
		[Serializable]
		public class Pool {
			[SerializeField]
			private T origin;
			private int currentIndex;
			private int currentSize;
			private int addNum;
			private Transform parent;
			private List<T> pool;

			/// <summary>
			/// コンストラクタ
			/// </summary>
			public Pool(T origin, int initNum, int addNum, Transform parent) {
				this.origin = origin;
				this.currentIndex = 0;
				this.currentSize = 0;
				this.addNum = addNum;
				this.parent = parent;
				this.pool = new List<T>();
				AddObjects(initNum);
			}

			/// <summary>
			/// オブジェクトの追加
			/// </summary>
			private void AddObjects(int num) {
				for (int i = 0; i < num; ++i) {
					T obj = Instantiate(origin);
					obj.name = origin.name;
					obj.transform.SetParent(parent);
					obj.gameObject.SetActive(false);
					pool.Add(obj);
				}
				currentSize = pool.Count;
			}

			/// <summary>
			/// オブジェクトの取得
			/// </summary>
			public T GetObject(Vector3 position) {
				T obj;
				for (int i = 0; i < currentSize; ++i) {
					currentIndex = (currentIndex + 1) % currentSize;
					obj = pool[currentIndex];
					if (!obj.gameObject.activeInHierarchy) {
						return ActivateObj(obj, position); ;
					}
				}
				//要素の追加
				currentIndex = currentSize;
				AddObjects(addNum);
				return ActivateObj(pool[currentIndex], position); ;
			}

			/// <summary>
			/// オブジェクトの有効化
			/// </summary>
			private T ActivateObj(T obj, Vector3 position) {
				obj.gameObject.SetActive(true);
				obj.transform.position = position;
				obj.InitPoolable();
				return obj;
			}

			/// <summary>
			/// 複数オブジェクトの取得
			/// </summary>
			public T[] GetObjects(int num, Vector3 position) {
				T[] objs = new T[num];
				for (int i = 0; i < num; ++i) {
					objs[i] = GetObject(position);
				}
				return objs;
			}
		}

		[SerializeField, Range(1, 128)]
		private int initNum = 16;
		[SerializeField, Range(1, 128)]
		private int addNum = 16;

		[Header("初期登録オブジェクト")]
		[SerializeField]
		private T[] initRegists;

		private Dictionary<string, Pool> poolDic;

		#region UnityEvent

		private void Awake() {
			Init();
		}

		#endregion

		#region Function

		/// <summary>
		/// 初期化
		/// </summary>
		private void Init() {
			poolDic = new Dictionary<string, Pool>();
			foreach (var t in initRegists) {
				RegistObject(t);
			}
		}

		/// <summary>
		/// オブジェクトの登録とプールの作成
		/// </summary>
		public Pool RegistObject(T obj) {
			if (obj == null) return null;
			if (!poolDic.ContainsKey(obj.name)) {
				Pool pool = new Pool(obj, initNum, addNum, transform);
				poolDic.Add(obj.name, pool);
				return pool;
			} else {
				return GetPool(obj);
			}
		}

		/// <summary>
		/// プールの取得
		/// </summary>
		public Pool GetPool(T obj) {
			if (poolDic.ContainsKey(obj.name)) {
				return poolDic[obj.name];
			} else {
				return null;
			}
		}

		/// <summary>
		/// プールの取得
		/// </summary>
		public Pool GetPool(string name) {
			if (poolDic.ContainsKey(name)) {
				return poolDic[name];
			} else {
				return null;
			}
		}

		/// <summary>
		/// オブジェクトの取得
		/// </summary>
		public T GetObject(string name, Vector3 position) {
			if (poolDic.ContainsKey(name)) {
				return poolDic[name].GetObject(position);
			} else {
				return null;
			}
		}

		#endregion
	}
}