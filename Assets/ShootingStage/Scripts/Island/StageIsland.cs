using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using ShootingShip.Factory;
using ShootingShip.Structure;
using ShootingStage.AttackableEntity;
using ShootingUtility.ValueIndicator;
using ShootingShip.Manager;
using ShootingUtility.ObjectDetector;
using ShootingShip.Attacker;
using ShootingShip.Example;

namespace ShootingStage.Island {

	public class FactoryLineEvent : UnityEvent<FactoryLine, ShipStructure> { }

	/// <summary>
	/// 生産ライン
	/// </summary>
	[Serializable]
	public class FactoryLine {

		[SerializeField]
		private string description;
		[SerializeField]
		private ShipFactory factory;
		[SerializeField]
		private int createLimit;
		private int create;
		private bool islimited;
		[SerializeField]
		private float interval;
		private float tInterval;

		public float intervalRatio { get { return tInterval / interval; } }

		//コールバック
		private FactoryLineEvent onCreate;
		public FactoryLineEvent OnCreate { get { return onCreate; } }

		#region Constructor

		public FactoryLine() {
			islimited = false;
			create = 0;
			tInterval = 0f;
			onCreate = new FactoryLineEvent();
		}

		#endregion

		#region Function

		/// <summary>
		/// ラインの更新
		/// </summary>
		public void Update() {
			if (islimited) return;
			tInterval += Time.deltaTime;
			if (tInterval > interval) {
				tInterval -= interval;
				if (factory) {
					Create();
				}
			}
		}

		/// <summary>
		/// 作成
		/// </summary>
		private void Create() {
			var ship = factory.CreateRandom();
			var enemy = ship.gameObject.AddComponent<AILeaderShip>();
			enemy.SetStructure(ship);
			enemy.TargetTag = "Player";
			if (onCreate != null) onCreate.Invoke(this, ship);
			create++;
			if (create >= createLimit) islimited = true;
		}

		/// <summary>
		/// 破棄
		/// </summary>
		public void Destroy() {
			if (create <= 0) return;
			create--;
			if (createLimit > create) islimited = false;
		}

		#endregion
	}

	/// <summary>
	/// ステージ上の島
	/// </summary>
	public class StageIsland : StageAttackableObject {

		[Header("プレイヤー")]
		[SerializeField]
		private string playerTag = "Player";

		[Header("ライン")]
		[SerializeField]
		private FactoryLine[] lines;

		[Header("ライン率表示")]
		[SerializeField]
		private Vector3 ratioOffset;
		[SerializeField]
		private Vector3 ratioInterval;
		private FloatIndicator[] ratioIndicators;
		private bool isShowedRatioIndicator = false;

		private StageManager sManager;
		private Dictionary<int, FactoryLine> createObjDic;
		private FloatIndicator hpIndicator;
		private bool isDied;

		#region UnityEvent

		private void Update() {
			if (!isDied) {
				UpdateLine();
			}
		}

		#endregion

		#region Line

		/// <summary>
		/// ラインの更新
		/// </summary>
		private void UpdateLine() {
			foreach (var l in lines) {
				l.Update();
			}
			if (isShowedRatioIndicator) {
				for (int i = 0; i < ratioIndicators.Length; ++i) {
					ratioIndicators[i].SetRatio(lines[i].intervalRatio);
				}
			}
		}

		/// <summary>
		/// ライン率の表示
		/// </summary>
		private void ShowRatioIndicator() {
			if (isShowedRatioIndicator) return;
			if (ratioIndicators == null) ratioIndicators = new FloatIndicator[lines.Length];
			if (sManager && sManager.IndicatorPool) {
				for (int i = 0; i < ratioIndicators.Length; ++i) {
					ratioIndicators[i] = sManager.IndicatorPool.GetObject();
					ratioIndicators[i].transform.position = transform.position + ratioOffset + ratioInterval * i;
					ratioIndicators[i].SetRatio(lines[i].intervalRatio);
				}
			}
			isShowedRatioIndicator = true;
		}

		/// <summary>
		/// ライン率の非表示
		/// </summary>
		private void HideRatioIndicator() {
			if (!isShowedRatioIndicator) return;
			if (ratioIndicators == null) return;
			for (int i = 0; i < ratioIndicators.Length; ++i) {
				if (ratioIndicators[i]) {
					ratioIndicators[i].gameObject.SetActive(false);
				}
			}
			isShowedRatioIndicator = false;
		}

		/// <summary>
		/// ラインの初期化
		/// </summary>
		private void InitLines() {
			foreach (var l in lines) {
				l.OnCreate.RemoveListener(OnCreate);
				l.OnCreate.AddListener(OnCreate);
			}
		}

		#endregion

		#region Callback

		/// <summary>
		/// 検知された
		/// </summary>
		private void OnDetected(ObjectDetector2D<Rigidbody2D> detector) {
			ShowRatioIndicator();
		}

		/// <summary>
		/// 解除された
		/// </summary>
		private void OnReleased(ObjectDetector2D<Rigidbody2D> detector) {
			HideRatioIndicator();
		}

		/// <summary>
		/// 機体の作成
		/// </summary>
		private void OnCreate(FactoryLine line, ShipStructure ship) {
			//辞書への追加とコールバックの設定
			createObjDic.Add(ship.gameObject.GetHashCode(), line);
			if (ship.Attackable) {
				ship.Attackable.OnDied.RemoveListener(OnShipDied);
				ship.Attackable.OnDied.AddListener(OnShipDied);
			}
			ship.transform.position = transform.position;
		}

		/// <summary>
		/// 機体の削除
		/// </summary>
		private void OnShipDied(AttackableObject2D attackable, ObjectAttacker2D attacker) {
			//ラインに伝える
			int hash = attackable.gameObject.GetHashCode();
			if (createObjDic.ContainsKey(hash)) {
				createObjDic[hash].Destroy();
			}
		}

		/// <summary>
		/// 外部からの攻撃
		/// </summary>
		private void OnAttacked(ObjectAttacker2D attacker) {
			//インディケータの更新
			if (hpIndicator) {
				hpIndicator.SetRatio(attackable.hpRatio);
			} else {
				if(sManager) {
					hpIndicator = sManager.IndicatorPool.GetObject();
					hpIndicator.transform.position = transform.position;
					hpIndicator.SetRatio(attackable.hpRatio);
				}
			}
		}

		/// <summary>
		/// 外部からの攻撃による死
		/// </summary>
		private void OnDied(AttackableObject2D attackable, ObjectAttacker2D attacker) {
			if (isDied) return;
			isDied = true;
			//インディケータの非表示
			hpIndicator.gameObject.SetActive(false);
			HideRatioIndicator();
			//検出関連を待機状態へ
			detectable.Standby();
		}

		#endregion

		#region IPoolable

		/// <summary>
		/// 初期化
		/// </summary>
		public override void InitPoolable() {
			base.InitPoolable();
			sManager = StageManager.Instance;
			createObjDic = new Dictionary<int, FactoryLine>();
			isDied = false;
			if (detectable) {
				detectable.OnDetected.RemoveListener(OnDetected);
				detectable.OnDetected.AddListener(OnDetected);
				detectable.OnReleased.RemoveListener(OnReleased);
				detectable.OnReleased.AddListener(OnReleased);
			}
			if (attackable) {
				attackable.OnAttacked.RemoveListener(OnAttacked);
				attackable.OnAttacked.AddListener(OnAttacked);
				attackable.OnDied.RemoveListener(OnDied);
				attackable.OnDied.AddListener(OnDied);
			}
			InitLines();
		}

		#endregion
	}
}