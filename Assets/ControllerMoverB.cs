using UnityEngine;
using InControl;
using Audionauts.Extensions;

public class ControllerMoverB : MonoBehaviour {
	
	public AxisSource axisSource;
	
	public float radius;

	public float speed;

	public int controller;
	
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

		transform.localPosition = Vector2.ClampMagnitude (transform.localPosition, radius);

	}
}
