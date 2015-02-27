using UnityEngine;
using System.Collections;
using InControl;

public class BallControl : MonoBehaviour {

	public float acc = 10;

	void FixedUpdate() {
		var inputDevice = InputManager.ActiveDevice;
		rigidbody.AddForce (new Vector3(inputDevice.LeftStick.X, 0, inputDevice.LeftStick.Y) * acc);
	}
	
	void OnCollisionEnter (Collision col) {
		var note = col.gameObject.GetComponent<Note> ();
		if (note != null) 
			note.remove();
	}
}
