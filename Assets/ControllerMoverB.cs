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


		transform.Translate (axis.Vector * speed
//		                     * Time.deltaTime
		                     );

		transform.localPosition = Vector2.ClampMagnitude (transform.localPosition, radius);

	}
}
