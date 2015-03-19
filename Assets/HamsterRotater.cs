using UnityEngine;
using System.Collections;
using InControl;

public class HamsterRotater : HamsterPhysic
{
	public int rotateAxis;
	public float rotateAcceleration = 2000;
	public float maxVelocity = 10;
	public float friction = 0.01f;
	public int controller;

	public HamsterPhysic wheel;

	public override void FixedUpdate ()
	{
		if (wheel != null) {
			// Calculate gravity
			var v = transform.rotation * Vector3.up; // Vector where x and y is cos and sin of the angle
			var f = Mass * g * v.x;
			angularVelocity += f / Mass * Time.deltaTime;

			var combinedForce = angularVelocity * Mass + wheel.angularVelocity * wheel.Mass;
			var combinedVelocity = combinedForce / (Mass + wheel.Mass);
			angularVelocity += (combinedVelocity - angularVelocity) * friction;
			wheel.angularVelocity += (combinedVelocity - wheel.angularVelocity) * friction;
		}
		base.FixedUpdate();
	}

	// Update is called once per frame
	void Update ()
	{
		var device = ControllerManager.Controllers[controller];
		var axis = rotateAxis == 0 ? device.LeftStick : rotateAxis == 1 ? device.RightStick : device.DPad;
		if (axis.X > 0.1f || axis.X < -0.1f) {
			if (axis.X < 0 && angularVelocity > -maxVelocity || axis.X > 0 && angularVelocity < maxVelocity)
				angularVelocity += axis.X * rotateAcceleration * Time.deltaTime;
		} 
	}
}
