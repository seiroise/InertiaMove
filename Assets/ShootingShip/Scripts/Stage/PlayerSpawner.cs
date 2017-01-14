using UnityEngine;
using System;
using ShootingShip.Factory;
using ShootingShip.Example;

namespace ShootingShip.Stage {

	/// <summary>
	/// プレイヤーのスポーナ
	/// </summary>
	public class PlayerSpawner : MonoBehaviour {

		[SerializeField]
		private ShipFactory factory;

		#region UnityEvent

		private void OnGUI() {
			if(GUI.Button(new Rect(0f, 200f, 100f, 100f), "スポーン")) {
				Spawn();
			}
		}

		#endregion

		#region Function

		/// <summary>
		/// スポーン
		/// </summary>
		public void Spawn() {
			if(factory) {
				var st = factory.CreateRandom("Player");
				var ship = st.gameObject.AddComponent<ExampleShip>();
				ship.SetStructure(st);
			}
		}

		#endregion
	}
}