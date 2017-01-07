using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingUtility.ObjectPool {

	/// <summary>
	/// 抽象オブジェクトプール
	/// </summary>
	public abstract class AbstractObjectPool<T> : MonoBehaviour where T : Component, IPoolable {

		[SerializeField]
		private T origin;

		[Header("生成パラメータ")]
		[SerializeField, Range(1, 256)]
		private int initNum = 16;
		[SerializeField, Range(0, 256)]
		private int addNum = 8;

		private List<T> pool;
		private int currentIndex;
		private int currentSize;

		#region UnityEvent

		private void Awake() {
			Initialize();
		}

		#endregion

		#region Function

		/// <summary>
		/// 初期化
		/// </summary>
		private void Initialize() {
			pool = new List<T>();
			currentIndex = -1;	//最初は-1
			currentSize = 0;
			AddObjects(initNum);
		}

		/// <summary>
		/// オブジェクトの追加
		/// </summary>
		private void AddObjects(int num) {
			for (int i = 0; i < num; ++i) {
				GameObject obj = Instantiate(origin.gameObject);
				obj.name = origin.name;
				obj.transform.SetParent(transform);
				obj.SetActive(false);
				pool.Add(obj.GetComponent<T>());
			}
			currentSize = pool.Count;
		}

		/// <summary>
		/// オブジェクトの取得
		/// </summary>
		public T GetObject() {
			T obj;
			for (int i = 0; i < currentSize; ++i) {
				currentIndex = (currentIndex + 1) % currentSize;
				obj = pool[currentIndex];
				if (!obj.gameObject.activeInHierarchy) {
					obj.gameObject.SetActive(true);
					obj.InitPoolable();
					return obj;
				}
			}
			//要素の追加
			currentIndex = currentSize;
			AddObjects(addNum);
			obj = pool[currentIndex];
			obj.gameObject.SetActive(true);
			return obj;
		}

		/// <summary>
		/// 複数オブジェクトの取得
		/// </summary>
		public T[] GetObjects(int num) {
			T[] objs = new T[num];
			for (int i = 0; i < num; ++i) {
				objs[i] = GetObject();
			}
			return objs;
		}

		#endregion
	}
}