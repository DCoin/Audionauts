using UnityEngine;
using InControl;
using Audionauts.Extensions;

public class ControllerMoverB : MonoBehaviour {
	
	public AxisSource axisSource;
	
	public float radius;

	public float speed;
	
	void Update () {
		
		InputDevice device = InputManager.ActiveDevice;
		TwoAxisInputControl axis = device.GetAxis (axisSource);


		transform.Translate (axis.Vector * speed
//		                     * Time.deltaTime
		                     );

		transform.localPosition = Vector2.ClampMagnitude (transform.localPosition, radius);

	}
}
