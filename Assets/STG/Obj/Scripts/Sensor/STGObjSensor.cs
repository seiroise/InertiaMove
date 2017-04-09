using UnityEngine;
using System;
using STG.Obj.Targeting;
using STG.BaseUtility.ComSystem;

namespace STG.Obj.Sensor {

	/// <summary>
	/// STGオブジェクト用の他STGオブジェクトセンサ
	/// </summary>
	[AddComponentMenu("STG/Obj/Sensor/STGObjSensor")]
	public class STGObjSensor : STGCom {

		[Header("Sensor")]
		[SerializeField]
		private DetectableSTGObj _detectable;
		[SerializeField]
		private STGObjDetector _detector;

	}
}