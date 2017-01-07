using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingUtility.ObjectPool;

namespace ShootingShip.Bullet {

	/// <summary>
	/// 弾のオブジェクトプール
	/// </summary>
	public class BulletPoolDictionary : ObjectPoolDictionary<ShootingBullet> { }
}