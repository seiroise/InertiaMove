using UnityEngine;
using System;
using ShootingShip.Utility;
using ShootingUtility.BackgroundScroll;
using MassLine;
using ShootingShip.Bullet;
using ShootingUtility.InputDetector;
using ShootingUtility.Lerp;
using ShootingUtility.ValueIndicator;
using ShootingUtility.Particle;

namespace ShootingShip.Manager {	

	/// <summary>
	/// ステージの管理者
	/// </summary>
	public class StageManager : SingletonMonoBehaviour<StageManager> {

		[SerializeField]
		private SwipeDetector swipe;
		public SwipeDetector Swipe { get { return swipe; } }

		[SerializeField]
		private FlickDetector flick;
		public FlickDetector Flick { get { return flick; } }

		[SerializeField]
		private LerpTracker playerTracker;
		public LerpTracker PlayerTracker { get { return playerTracker; } }

		[SerializeField]
		private BackgroundPlacer bgPlacer;
		public BackgroundPlacer BgPlacer { get { return bgPlacer; } }

		[SerializeField]
		private MassLineFactory lineFactory;
		public MassLineFactory LineFactory { get { return lineFactory; } }

		[SerializeField]
		private BulletPoolDictionary bulletPool;
		public BulletPoolDictionary BulletPool { get { return bulletPool; } }

		[SerializeField]
		private FloatIndicatorPool indicatorPool;
		public FloatIndicatorPool IndicatorPool { get { return indicatorPool; } }

		[SerializeField]
		private ParticlePoolDictionary particlePool;
		public ParticlePoolDictionary ParticlePool { get { return particlePool; } }
	}
}