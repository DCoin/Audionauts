using UnityEngine;
using InControl;

public class ControllerRotater : MonoBehaviour {

	public int rotateAxis;

	public float deadZoneSize = 0.1f;

	void Update () {

		InputDevice device = InputManager.ActiveDevice;
		TwoAxisInputControl axis = rotateAxis == 0 ? device.LeftStick : rotateAxis == 1 ? device.RightStick : device.DPad;

		if (axis.Vector.sqrMagnitude > deadZoneSize) {
			transform.localRotation = Quaternion.FromToRotation (Vector3.up, -axis.Vector);
		}

	
	}
}
