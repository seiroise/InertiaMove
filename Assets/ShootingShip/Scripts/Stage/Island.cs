using UnityEngine;
using System;
using ShootingShip.Factory;
using ShootingShip.Example;
using ShootingShip.Attacker;
using ShootingUtility.ValueIndicator;
using ShootingShip.Manager;
using ShootingUtility.ObjectDetector;

namespace ShootingShip.Stage {

	/// <summary>
	/// 生産ライン
	/// </summary>
	[Serializable]
	public class FactoryLine {
		[SerializeField]
		public string tag;
		[SerializeField]
		public float interval;
		private float tInterval;

		#region Function

		/// <summary>
		/// ラインの更新、生産する場合はtrueを返す
		/// </summary>
		public bool Update() {
			tInterval += Time.deltaTime;
			if (tInterval > interval) {
				tInterval = 0f;
				return true;
			} else {
				return false;
			}
		}

		#endregion
	}

	/// <summary>
	/// 島
	/// </summary>
	[RequireComponent(typeof(Collider2D), typeof(AttackableObject2D), typeof(DetectableObject2D))]
	public class Island : MonoBehaviour {

		[SerializeField]
		private ShipFactory factory;
		//[SerializeField]
		public FactoryLine[] lines;

		private Collider2D collider2D;
		private AttackableObject2D attackable;
		private DetectableObject2D detectable;
		private FloatIndicator indicator;

		#region UnityEvent

		private void Start() {
			collider2D = GetComponent<Collider2D>();
			attackable = GetComponent<AttackableObject2D>();
			if (attackable) {
				attackable.OnAttacked.RemoveListener(OnAttacked);
				attackable.OnAttacked.AddListener(OnAttacked);
				attackable.OnDied.RemoveListener(OnDied);
				attackable.OnDied.AddListener(OnDied);
			}
			detectable = GetComponent<DetectableObject2D>();
		}

		private void Update() {
			UpdateLine();
		}

		#endregion

		#region Function

		/// <summary>
		/// 生産ラインの更新
		/// </summary>
		public void UpdateLine() {
			foreach (var line in lines) {
				if (line.Update()) {
					//とりあえずテスト
					//本当は装備の強さをかを考慮する
					var st = factory.CreateRandom();
					var ship = st.gameObject.AddComponent<AILeaderShip>();
					ship.SetStructure(st);
					ship.TargetTag = "Player";
				}
			}
		}

		#endregion

		#region Callback

		/// <summary>
		/// 攻撃された
		/// </summary>
		private void OnAttacked(ObjectAttacker2D attacker) {
			if (indicator) {
				//値の反映
				indicator.SetRatio(attackable.HP, attackable.NowHP);
			} else {
				//表示器の取得
				var sManager = StageManager.Instance;
				if (sManager && sManager.IndicatorPool) {
					indicator = sManager.IndicatorPool.GetObject();
					indicator.Tracker.Target = transform;
					indicator.SetRatio(attackable.HP, attackable.NowHP);
				}
			}
		}

		/// <summary>
		/// 倒された
		/// </summary>
		private void OnDied(ObjectAttacker2D attacker) {
			attackable.enabled = false;
			collider2D.enabled = false;
			if (detectable) {
				detectable.ReleaseAllDetector();
			}
			indicator.gameObject.SetActive(false);
		}

		#endregion
	}
}