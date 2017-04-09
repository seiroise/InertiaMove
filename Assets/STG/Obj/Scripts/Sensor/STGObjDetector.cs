using UnityEngine;
using System;
using System.Collections.Generic;
using STG.BaseUtility.ObjectDetector;

namespace STG.Obj.Sensor {

	/// <summary>
	/// STGオブジェクト検出器
	/// </summary>
	[AddComponentMenu("STG/Obj/Sensor/STGObjDetector")]
	public class STGObjDetector : ObjectDetector2D<STGObj> { }
}