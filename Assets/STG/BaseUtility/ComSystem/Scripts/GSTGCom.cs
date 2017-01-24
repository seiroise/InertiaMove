using UnityEngine;
using System;
using System.Collections.Generic;

namespace STG.BaseUtility.ComSystem {
	
	/// <summary>
	/// テスト用ジェネリックSTGコンポーネントマネージャ
	/// </summary>
	public class GSTGComManager<Com> : MonoBehaviour where Com : GSTGCom<GSTGComManager<Com>> {
		
	}

	/// <summary>
	/// テスト用ジェネリックSTGコンポーネント
	/// </summary>
	public class GSTGCom<Manager> : MonoBehaviour where Manager : GSTGComManager<GSTGCom<Manager>>{
		
	}

	public class GSTGWeapon : GSTGComManager<GSTGWeaponCom> {
		
	}


	public class GSTGWeaponCom : GSTGCom<GSTGWeapon> {
		
	}
}