using UnityEngine;
using System;
using System.Collections.Generic;

namespace STG.BaseUtility.Singleton {

	/// <summary>
	/// シングルトン
	/// </summary>
	public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

		private static T instance;
		public static T Instance {
			get {
				if (instance == null) {
					instance = (T)FindObjectOfType<T>();
					if (instance == null) {
						Debug.LogError(typeof(T) + " is nothing");
					}
				}
				return instance;
			}
		}

		#region Function

		protected virtual void Awake() {
			if (this != instance && instance != null) {
				Destroy(this);
				return;
			}
		}

		#endregion
	}
}