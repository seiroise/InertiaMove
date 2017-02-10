using UnityEngine;
using ShootingUtility.ObjectDetector;
using STG.Obj.Targeting;
using STG.Obj.Armor;
using STG.BaseUtility.ComSystem;
using STG.BaseUtility.Attack;

namespace STG.Obj.Marker {

	/// <summary>
	/// STGオブジェクト用のマーカー
	/// </summary>
	[RequireComponent(typeof(DetectableSTGObj))]
	public class STGObjMarker : STGCom {

		[SerializeField]
		private SpriteRenderer markerRenderer;
		public SpriteRenderer MarkerRenderer { get { return markerRenderer; } }

		[Header("Color")]
		[SerializeField]
		private Color dieColor = Color.gray;

		private DetectableSTGObj detectable;
		public DetectableSTGObj Detectable { get { return detectable; } }

		private STGObjArmor armor;

		#region VirtualFunction

		/// <summary>
		/// 初期化
		/// </summary>
		public override void STGInit(STGComManager manager) {
			base.STGInit(manager);
			detectable = GetComponent<DetectableSTGObj>();
		}

		/// <summary>
		/// 起動
		/// </summary>
		public override void STGAwake() {
			base.STGAwake();
			armor = manager.GetCom<STGObjArmor>();
			if (armor) {
				armor.Armor.OnDied.RemoveListener(OnDied);
				armor.Armor.OnDied.AddListener(OnDied);
			}
		}

		#endregion

		#region Callback

		/// <summary>
		/// 装甲0
		/// </summary>
		private void OnDied(AttackableObject2D attackable, ObjectAttacker2D attacker) {
			if (markerRenderer) {
				markerRenderer.color = dieColor;
			}
			if (detectable) {
				detectable.Standby();
			}
		}

		#endregion
	}
}