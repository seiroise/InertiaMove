using UnityEngine;
using System;
using ShootingShip.Utility;
using ShootingStage.Island;

namespace ShootingStage.Manager {

	/// <summary>
	/// ステージの管理者
	/// </summary>
	public class StageManager : SingletonMonoBehaviour<StageManager> {

		[SerializeField]
		private IslandParameterSeed parameterSeed;
		public IslandParameterSeed ParameterSeed { get { return parameterSeed; } }
	}
}