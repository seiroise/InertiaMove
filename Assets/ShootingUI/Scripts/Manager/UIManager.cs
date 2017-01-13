using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Utility;
using ShootingUI.Ship;

namespace ShootingUI.Manager {
	
	/// <summary>
	/// UIの管理者
	/// </summary>
	public class UIManager : SingletonMonoBehaviour<UIManager>{

		[SerializeField]
		private UIShipQuickBar shipQuickBar;
		public UIShipQuickBar ShipQuickBar { get { return shipQuickBar; } }
	}
}