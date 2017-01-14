using UnityEngine;
using System;
using System.Collections.Generic;
using ShootingShip.Equipment;

namespace ShootingShip.Structure {

	/// <summary>
	/// 機体の装備の操作
	/// 型パラメータはInspectorに表示したいので仕方なく２つに
	/// </summary>
	public abstract class ShipEquipmentController<Equipment, Holder> : ShipComManager<Holder>
		where Equipment : ShipEquipment
		where Holder : ShipEquipmentHolder<Equipment> {
	
	}
}