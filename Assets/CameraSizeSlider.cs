using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ShootingUtility.Lerp;

public class CameraSizeSlider : MonoBehaviour {

	public Slider slider;

	public LerpCameraSize lerpSize;

	private void Start() {
		if (slider) {
			slider.onValueChanged.AddListener(OnValueChanged);
		}
	}

	private void OnValueChanged(float value) {
		if (lerpSize) {
			lerpSize.SetCameraSize(value);
		}
	}
}