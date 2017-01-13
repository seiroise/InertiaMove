using UnityEngine;
using System;

namespace ShootingStage.Island {

	/// <summary>
	/// 島を生成するためのパラメータ
	/// </summary>
	[Serializable]
	public class IslandParameter {

		[SerializeField]
		private int durable;    //耐久力
		public int Durable { get { return durable; } }
		[SerializeField]
		private int attack;     //攻撃力(兵装力)
		public int Attack { get { return attack; } }
		[SerializeField]
		private int produce;    //生産力
		public int Produce { get { return produce; } }
		[SerializeField]
		private int defence;    //防衛力
		public int Defence { get { return defence; } }

		public IslandParameter(int durable, int attack, int produce, int defence) {
			this.durable = durable;
			this.attack = attack;
			this.produce = produce;
			this.defence = defence;
		}
	}
}