using UnityEngine;
using System.Collections;
using STG.Obj;
using ShootingUtility.ObjectDetector;

public class TargetingDemo : MonoBehaviour {

	public STGObj playerShip;

	#region UnityEvent

	private void Start() {
		//コールバックの設定
		if (playerShip) {
			playerShip.TargetingResolver.Detector.OnDetect.RemoveListener(OnObjDetect);
			playerShip.TargetingResolver.Detector.OnDetect.AddListener(OnObjDetect);
			playerShip.TargetingResolver.Detector.OnRelease.RemoveListener(OnObjRelease);
			playerShip.TargetingResolver.Detector.OnRelease.AddListener(OnObjRelease);
		}
	}

	private void Update() {

	}

	#endregion

	#region Callback

	private void OnObjDetect(STGObj obj) {
		Debug.Log("Detected : " + obj);
	}

	private void OnObjRelease(STGObj obj) {
		Debug.Log("Released : " + obj);
	}

	#endregion
}