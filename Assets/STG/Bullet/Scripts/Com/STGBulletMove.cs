using UnityEngine;
using System;
using System.Collections.Generic;

namespace STG.Bullet.Com {

	/// <summary>
	/// STGバレット用の移動コンポーネント
	/// </summary>
	public class STGBulletMove : STGBulletCom {

		[SerializeField, Range(0.01f, 200f)]
		protected float speed = 50f;
		public float Speed { get { return speed; } set { speed = value; } }
	}
}