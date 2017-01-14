using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ShootingShip.Equipment;
using ShootingShip.DataObject;

namespace ShootingUI.Ship {

	/// <summary>
	/// 機体の装備の表示
	/// </summary>
	public class UIEquipmentIndicator : MonoBehaviour {

		protected ShipEquipment equipment;

		[Header("UI")]
		[SerializeField]
		protected Image equipmentImage;
		[SerializeField]
		protected Text equipmentName;

		[Header("Image")]
		[SerializeField]
		private Sprite lockImage;

		[Header("Ratio Bar")]
		[SerializeField]
		private Image ratioBar;
		public Image RatioBar { get { return ratioBar; } }

		#region UnityEvent

		private void Update() {
			if(equipment) equipment.UpdateIndicator(this);
		}

		#endregion

		#region Function

		/// <summary>
		/// 画像とテキストの設定
		/// </summary>
		public void SetImageAndText(Sprite image, string text) {
			if (equipmentImage) {
				equipmentImage.sprite = image;
			}
			if (equipmentName) {
				equipmentName.text = text;
			}
		}

		/// <summary>
		/// 装備データオブジェクトからの画像とテキストの設定
		/// </summary>
		public void SetImageAndText(EquipmentDataObject dataObj) {
			if(dataObj) {
				SetImageAndText(dataObj.DescriptionImage, dataObj.DescriptionName);
			}
		}

		/// <summary>
		/// 装備を閉じる(装備できない状態へ)
		/// </summary>
		public void LockEquipment() {
			SetImageAndText(lockImage, "Lock");
		}

		/// <summary>
		/// 装備を開ける(装備できる状態へ)
		/// </summary>
		public void UnlockEquipment() {
			SetImageAndText(null, "None");
		}

		#endregion

		#region VirtualFunction

		/// <summary>
		/// 装備の設定(装備してる状態へ)
		/// </summary>
		public virtual void SetEquipment(ShipEquipment equipment) {
			this.equipment = equipment;
			var dataObj = equipment.EquipmentData;
			if (dataObj) {
				SetImageAndText(dataObj);
			} else {
				SetImageAndText(null, "No Data");
			}
		}

		/// <summary>
		/// 装備の解除(装備できる状態へ)
		/// </summary>
		public virtual void RemoveEquipment() {
			SetImageAndText(null, "None");
		}

		#endregion
	}
}