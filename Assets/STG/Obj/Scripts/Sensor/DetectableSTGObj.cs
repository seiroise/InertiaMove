using UnityEngine;
using System;
using System.Collections.Generic;
using STG.BaseUtility.ObjectDetector;

namespace STG.Obj.Sensor {

	/// <summary>
	/// 検出可能STGオブジェクト
	/// </summary>
	[AddComponentMenu("STG/Obj/Sensor/DetectableSTGObj")]
	public class DetectableSTGObj : DetectableObject2D<STGObj> { }
}