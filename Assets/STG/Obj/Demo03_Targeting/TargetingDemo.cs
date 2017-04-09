using UnityEngine;
using System.Collections;
using STG.Obj;
using ShootingUtility.ObjectDetector;
using STG.BaseUtility.ObjectDetector;

public class TargetingDemo : MonoBehaviour {

	public STGObj playerShip;

	#region UnityEvent

	private void Start() {
		//コールバックの設定
		if (playerShip) {
			playerShip.targetingResolver.Detector.OnDetect.RemoveListener(OnObjDetect);
			playerShip.targetingResolver.Detector.OnDetect.AddListener(OnObjDetect);
			playerShip.targetingResolver.Detector.OnRelease.RemoveListener(OnObjRelease);
			playerShip.targetingResolver.Detector.OnRelease.AddListener(OnObjRelease);
		}
	}

	private void Update() {

	}

	#endregion

	#region Callback

	private void OnObjDetect(ObjectAttribute attr, STGObj obj) {
		Debug.Log("Detected : " + obj);
	}

	private void OnObjRelease(ObjectAttribute attr, STGObj obj) {
		Debug.Log("Released : " + obj);
	}

	#endregion
}