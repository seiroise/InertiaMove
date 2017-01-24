using UnityEngine;
using System;
using System.Collections.Generic;

namespace STG.Bullet.Com {
	
	public class STGBulletStraightMove : STGBulletMove {

		#region VirtualFunction

		public override void UpdateCom() {
			base.UpdateCom();
			transform.Translate(new Vector3(speed, 0f, 0f) * Time.deltaTime);
		}

		#endregion
	}
}