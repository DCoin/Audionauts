using UnityEngine;
using InControl;
using Audionauts.Extensions;

public class ControllerRotaterB : MonoBehaviour {

	public AxisSource axisSource;
	
	public float deadZoneSize = 0.1f;

	public float rotateMaxSpeed = 3;

	public int easingLevel = 2;

	private float easeIn(float v, int p) {

		float vs = Mathf.Sign(v);
		float va = v * vs;

		float vp = 1;
		for (int i = 0; i < p; ++i)
			vp *= va;

		return vs * vp;

	}

	void Update () {
		
		InputDevice device = InputManager.ActiveDevice;
		TwoAxisInputControl axis = device.GetAxis (axisSource);

		if (axis.Vector.sqrMagnitude > deadZoneSize) {

			Vector3 rot = -rotateMaxSpeed * easeIn(axis.Vector.x, easingLevel) * Vector3.forward;		

			transform.Rotate(rot);

		}

	}

}
