using UnityEngine;
using System;
using EditorUtility;

namespace ShootingStage.Island {

	/// <summary>
	/// 島パラメータシード
	/// </summary>
	[Serializable]
	public class IslandParameterSeed {

		[SerializeField, MinMaxRange(1, 20)]
		private MinMax durable;    //耐久力
		public MinMax Durable { get { return durable; } }
		[SerializeField, MinMaxRange(1, 20)]
		private MinMax attack;     //攻撃力(兵装力)
		public MinMax Attack { get { return attack; } }
		[SerializeField, MinMaxRange(1, 20)]
		private MinMax produce;    //生産力
		public MinMax Produce { get { return produce; } }
		[SerializeField, MinMaxRange(1, 20)]
		private MinMax defence;    //防衛力
		public MinMax Defence { get { return defence; } }

		#region Function

		/// <summary>
		/// ランダムにパラメータを作成
		/// </summary>
		public IslandParameter CreateRandomParameter() {
			return new IslandParameter(
				durable.randomInt, attack.randomInt,
				produce.randomInt, defence.randomInt);
		}

		#endregion
	}
}