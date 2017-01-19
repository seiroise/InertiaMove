using UnityEngine;
using System;

namespace STG.Obj.Thruster {

	/// <summary>
	/// STGオブジェクト用の推進器のパラメータ
	/// </summary>
	[Serializable]
	public class STGObjThrusterParameter {

		[SerializeField, Range(1f, 100f)]
		private float accele;				//加速
		public float Accele { get { return accele; } set { accele = value; } }
		[SerializeField, Range(0f, 5f)]
		private float maxCombustEfficiency;	//最大燃焼効率
		public float MaxCombustEfficiency { get { return maxCombustEfficiency; } set { maxCombustEfficiency = value; } }
	}
}