using UnityEngine;
using System;
using ShootingUtility.ComSystem;
using ShootingUtility.ObjectDetector;

namespace STG.Obj.Detector {

	/// <summary>
	/// STGオブジェクト用の検出器
	/// </summary>
	public class STGObjDetector : STGCom {

		[SerializeField]
		private RigidbodyDetector2D detector;
	}
}