using UnityEngine;
using InControl;
using Audionauts.Extensions;

public class ControllerMoverA : MonoBehaviour {
	
	public AxisSource axisSource;
	
	public float radius;
	
	void Update () {
		
		InputDevice device = InputManager.ActiveDevice;
		TwoAxisInputControl axis = device.GetAxis (axisSource);
		
		transform.localPosition = radius * axis.Vector;
		
	}
}
