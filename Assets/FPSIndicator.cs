using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSIndicator : MonoBehaviour {

	[SerializeField]
	private Text fps;

	IEnumerator Start() {
		while (true) {
			fps.text = (1f / Time.deltaTime).ToString();
			yield return new WaitForSeconds(1f);
		}
	}
}