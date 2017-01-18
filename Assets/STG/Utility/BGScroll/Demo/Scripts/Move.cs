using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	[SerializeField, Range(1f, 100f)]
	private float speed = 10f;

	private void Update () {

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(h, v) * speed * Time.deltaTime);
	}

	private void OnGUI() {
		GUILayout.Label("AWSDまたは←↑↓→で移動");
	}
}