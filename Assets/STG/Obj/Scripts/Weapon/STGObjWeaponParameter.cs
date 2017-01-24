using UnityEngine;
using System;
using System.Collections.Generic;

namespace STG.Obj.Weapon {

	/// <summary>
	/// STGオブジェクト用の武器のパラメータ
	/// </summary>
	[Serializable]
	public class STGObjWeaponParameter {

		[SerializeField, Range(1, 1000)]
		private int damage;
		public int Damage { get { return damage; } set { damage = value; } }
		[SerializeField, Range(0.01f, 10f)]
		private float interval;
		public float Interval { get { return interval; } set { interval = value; } }
		[SerializeField, Range(1, 200)]
		private float shotRange;
		public float ShotRange { get { return shotRange; } set { shotRange = value; } }
		[SerializeField, Range(1, 200)]
		private float shotSpeed;
		public float ShotSpeed { get { return shotSpeed; } set { shotSpeed = value; } }

		public float lifeTime { get { return shotRange / shotSpeed; } }
	}
}