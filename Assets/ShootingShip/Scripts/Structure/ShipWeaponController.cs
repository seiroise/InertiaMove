using UnityEngine;
using System;
using System.Collections.Generic;

namespace ShootingShip.Structure {

	/// <summary>
	/// 機体の武器の操作
	/// </summary>
	public class ShipWeaponController : ShipComManager<ShipWeaponHolder> {

		//ターゲッティング
		private Transform trans;
		private Transform targetTrans;		//目標
		private Vector3 prevPosition;		//前回座標
		private Vector3 targetDeviation;	//目標偏差

		#region UnityEvent

		private void Update() {
			Targeting();
		}

		#endregion

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitCom(ShipStructure structure) {
			base.InitCom(structure);
			trans = transform;
			structure.WeaponController = this;
		}

		#endregion

		#region Function

		/// <summary>
		/// ターゲッティング(偏差)
		/// </summary>
		private void Targeting() {
			if (!targetTrans) return;
			Vector3 tPos = targetTrans.position;
			//目標との距離を計算
			float distance = (tPos - trans.position).magnitude;
			//目標の移動量を計算
			targetDeviation = tPos - prevPosition;
			prevPosition = tPos;
			//目標座標を設定
			SetTargetAngle(tPos + targetDeviation * distance * 100f);
		}

		/// <summary>
		/// 弾に設定するタグの設定
		/// </summary>
		public void SetBulletExclusionTag(string tag) {
			foreach(var c in coms) {
				c.Equipment.BulletExclusionTag = tag;
			}
		}

		/// <summary>
		/// 目標との角度を設定
		/// </summary>
		public void SetTargetAngle(Vector3 wPosition) {
			foreach (var c in coms) {
				c.SetTargetAngle(wPosition);
			}
		}

		/// <summary>
		/// 目標の設定
		/// </summary>
		public void SetTarget(Transform target) {
			if (targetTrans != target) {
				if (target) {
					prevPosition = target.transform.position;
				}
				targetTrans = target;
			}
		}

		/// <summary>
		/// 全ての武器を起動
		/// </summary>
		public void WeaponAwake() {
			foreach (var c in coms) {
				c.Axis.IsLocal = false;
				c.Equipment.WeaponAwake();
			}
		}

		/// <summary>
		/// 全ての武器を待機
		/// </summary>
		public void WeaponStandby() {
			SetTarget(null);
			foreach (var c in coms) {
				c.Axis.IsLocal = true;
				c.SetAngle(0f);
				c.Equipment.WeaponStandby();
			}
		}

		#endregion
	}
}