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
	public class STGObj : STGComManager {

		//主要コンポーネント
		private STGObjMarker marker;
		public STGObjMarker Marker {
			get {
				if(!marker) marker = GetCom<STGObjMarker>();
				return marker;
			}
		}
		private STGObjTargetingResolver targetingResolver;
		public STGObjTargetingResolver TargetingResolver {
			get {
				if (!targetingResolver) targetingResolver = GetCom<STGObjTargetingResolver>();
				return targetingResolver;
			}
		}
		private STGObjArmor armor;
		public STGObjArmor Armor {
			get {
				if (!armor) armor = GetCom<STGObjArmor>();
				return armor;
			}
		}
		private STGObjAttitudeController attitudeCon;
		public STGObjAttitudeController AttitudeCon {
			get {
				if (!attitudeCon) attitudeCon = GetCom<STGObjAttitudeController>();
				return attitudeCon;
			}
		}
	}
}