using UnityEngine;
using System;
using System.Collections.Generic;
using STG.Obj;
using STG.Obj.Targeting;
using STG.BaseUtility.Lerp;

public class TargetingCamera : MonoBehaviour {

	public STGObj playerShip;
	public LerpCameraSize cameraSize;
	public float defaultCameraSize = 20f;
	public float addSize = 10f;

	private STGObjDetector detector;

	#region UnityEvent

	private void Start() {
		detector = playerShip.TargetingResolver.Detector;
		cameraSize.SetCameraSize(defaultCameraSize);
		//コールバック設定
		if (detector) {
			detector.OnDetect.RemoveListener(OnObjDetect);
			detector.OnDetect.AddListener(OnObjDetect);
			detector.OnRelease.RemoveListener(OnObjRelease);
			detector.OnRelease.AddListener(OnObjRelease);
		}
	}

	#endregion

	#region Callback

	private void OnObjDetect(STGObj obj) {
		int count = detector.GetDetectCount();
		if (count > 0) {
			cameraSize.SetCameraSize(detector.GetDetectAreaSize() + addSize);
		}
	}

	private void OnObjRelease(STGObj obj) {
		int count = detector.GetDetectCount();
		if (count == 0) {
			cameraSize.SetCameraSize(defaultCameraSize);
		}
	}

	#endregion
}