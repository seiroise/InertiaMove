using UnityEngine;
using STG.Obj.Targeting;
using STG.Obj.Armor;
using STG.BaseUtility.ComSystem;
using STG.BaseUtility.Attack;

namespace STG.Obj.Marker {

	/// <summary>
	/// STGオブジェクト用のマーカー
	/// </summary>
	[AddComponentMenu("STG/Obj/Marker/STGObjMarker")]
	public class STGObjMarker : STGCom {

		[SerializeField]
		private SpriteRenderer _markerRenderer;
		public SpriteRenderer markerRenderer { get { return _markerRenderer; } }

		[Header("Color")]
		[SerializeField]
		private Color _dieColor = Color.gray;

		private STGObjArmor _armor;

		#region VirtualFunction

		/// <summary>
		/// 起動
		/// </summary>
		public override void STGAwake() {
			base.STGAwake();
			_armor = manager.GetCom<STGObjArmor>();
			if (_armor) {
				_armor.armor.OnDied.RemoveListener(OnDied);
				_armor.armor.OnDied.AddListener(OnDied);
			}
		}

		#endregion

		#region Callback

		/// <summary>
		/// 装甲0
		/// </summary>
		private void OnDied(AttackableObject2D attackable, ObjectAttacker2D attacker) {
			if (_markerRenderer) {
				_markerRenderer.color = _dieColor;
			}
		}

		#endregion
	}
}