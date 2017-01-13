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
		public Sprite DescriptionImage { get { return descriptionImage; } }
		[SerializeField]
		private string descriptionName;
		public string DescriptionName { get { return descriptionName; } }
	}
}