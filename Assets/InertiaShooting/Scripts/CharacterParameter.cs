using System;

namespace InertiaShooting {

	/// <summary>
	/// キャラクターの持つパラメータ
	/// </summary>
	[Serializable]
	public struct CharacterParameter {

		public int armor;	//耐久
		public int attack;	//攻撃
		public int thrust;	//推力

		public CharacterParameter (int armor, int attack, int thrust) {
			this.armor = armor;
			this.attack = attack;
			this.thrust = thrust;
		}
	}
}