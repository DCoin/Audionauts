using UnityEngine;
using System.Collections;
using InControl;

public class Control : MonoBehaviour {

	void Update() {

	}

	void OnTriggerStay() {
		var inputDevice = InputManager.ActiveDevice;
		if (inputDevice.Action1.WasPressed) {
			Debug.Log("Do something to the ball");
		}
	}
}
