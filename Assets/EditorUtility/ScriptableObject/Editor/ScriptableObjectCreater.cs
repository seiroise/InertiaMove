using UnityEngine;
using UnityEditor;
using System.Collections;

public class ScriptableObjectCreater : MonoBehaviour {

	#region Function

	/// <summary>
	/// ScriptableObjectの作成
	/// </summary>
	[MenuItem("ScriptableObject/Create")]
	public static void Create() {

		var obj = new ScriptableObject();

		string path = Application.dataPath + "/ScriptableObject.aseet";
		AssetDatabase.CreateAsset(obj, path);
		AssetDatabase.Refresh();
	}

	#endregion
}