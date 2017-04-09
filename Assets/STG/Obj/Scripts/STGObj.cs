using UnityEngine;
using System;
using STG.Obj.Marker;
using STG.Obj.Armor;
using STG.Obj.Targeting;
using STG.Obj.Attitude;
using STG.Obj.Targeting;
using STG.BaseUtility.ComSystem;

namespace STG.Obj {

	/// <summary>
	/// STG用のオブジェクト
	/// </summary>
	[AddComponentMenu("STG/Obj/STGObj")]
	public class STGObj : STGComManager {

		//主要コンポーネント
		private STGObjMarker _marker;
		public STGObjMarker marker {
			get {
				if(!_marker) _marker = GetCom<STGObjMarker>();
				return _marker;
			}
		}
		private STGObjTargetingResolver _targetingResolver;
		public STGObjTargetingResolver targetingResolver {
			get {
				if (!_targetingResolver) _targetingResolver = GetCom<STGObjTargetingResolver>();
				return _targetingResolver;
			}
		}
		private STGObjArmor _armor;
		public STGObjArmor armor {
			get {
				if (!_armor) _armor = GetCom<STGObjArmor>();
				return _armor;
			}
		}
		private STGObjAttitudeController _attitudeCon;
		public STGObjAttitudeController attitudeCon {
			get {
				if (!_attitudeCon) _attitudeCon = GetCom<STGObjAttitudeController>();
				return _attitudeCon;
			}
		}
	}
}