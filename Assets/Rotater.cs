using UnityEngine;
using System.Collections;
using InControl;

public class Rotater : MonoBehaviour
{

	public KeyCode rotateLeft;
	public KeyCode rotateRight;
	public float rotateSpeed;
	public int rotateAxis;
	
	// Update is called once per frame
	void Update ()
	{

		Vector3 rot = rotateSpeed * Vector3.forward;

		var device = InputManager.ActiveDevice;
		var axis = rotateAxis == 0 ? device.LeftStick : rotateAxis == 1 ? device.RightStick : device.DPad;

		if (axis.Vector.sqrMagnitude > 0.1) {
			transform.localRotation = Quaternion.FromToRotation (Vector3.up, axis.Vector);
		} else {
			if (Input.GetKey (rotateLeft)) {
				transform.Rotate (-rot);
			}

			if (Input.GetKey (rotateRight)) {
				transform.Rotate (rot);
			}
		}
	}
}
