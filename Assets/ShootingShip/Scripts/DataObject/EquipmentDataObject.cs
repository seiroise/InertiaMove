using UnityEngine;
using System.Collections.Generic;

namespace ShootingShip.DataObject {

	/// <summary>
	/// 装備のデータオブジェクト
	/// </summary>
	public class EquipmentDataObject : ScriptableObject {

		[Header("Description")]
		[SerializeField]
		private Sprite descriptionImage;
		[SerializeField]
		private string name;
	}
}