using UnityEngine;
using System.Collections;
using STG.Obj;
using STG.Obj.Attitude;
using STG.Obj.Thruster;
using STG.Obj.Marker;

public class MoveDemo : MonoBehaviour {

	public STGObj obj;

	private STGObjAttitudeController attitudeCon;

	// Use this for initialization
	void Start() {
		if(obj) {
			attitudeCon = obj.GetCom<STGObjAttitudeController>();
		}
	}

	// Update is called once per frame
	void Update() {
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if(attitudeCon) {
			if(input.magnitude > 0.01f) {
				attitudeCon.Move(input, input.magnitude);
			} else {
				attitudeCon.Move(0f);
			}
		}
	}
}