using UnityEngine;
using InControl;
using Audionauts.Extensions;

public class ControllerMoverA : MonoBehaviour {
	
	public AxisSource axisSource;
	
	public float radius;

	public int controller;
	
	void Update () {
		
		InputDevice device = ControllerManager.Controllers[controller];
		TwoAxisInputControl axis = device.GetAxis (axisSource);
		
		transform.localPosition = radius * axis.Vector;
		
	}
}
