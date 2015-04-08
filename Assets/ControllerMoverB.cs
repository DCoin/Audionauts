using UnityEngine;
using InControl;
using Audionauts.Extensions;

public class ControllerMoverB : MonoBehaviour {
	
	public AxisSource axisSource;
	
	public float radius;

	public float speed;

	public int controller;

	public Vector2 disturbance = Vector2.zero;

	public float drag = 0.9f;
	
	void Update () {
		
		InputDevice device = ControllerManager.Controllers[controller];
		TwoAxisInputControl axis = device.GetAxis (axisSource);

		if(ControllerManager.Controllers[1] == InputDevice.Null) {

			device = ControllerManager.Controllers[0];

			if(controller == 0) {
				axis = device.GetAxis(AxisSource.StickLeft);

			} else if (controller == 1) {
				axis = device.GetAxis(AxisSource.StickRight);

			}

		}


		transform.Translate (axis.Vector * speed
//		                     * Time.deltaTime
		                     , Space.World);
		transform.Translate (disturbance, Space.World);
		disturbance *= drag;

		transform.localPosition = Vector2.ClampMagnitude (transform.localPosition, radius);

	}
}
