using UnityEngine;
using System.Collections;
using InControl;

public class Rotater : MonoBehaviour
{

	public KeyCode rotateLeft;
	public KeyCode rotateRight;
	public float rotateAcceleration;
	public float rotateMaxSpeed = 3;

	private float rotateSpeed = 0;

	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKey (rotateLeft)) {
			rotateSpeed = Mathf.Max(-rotateMaxSpeed, rotateSpeed - rotateAcceleration);
		} else if (Input.GetKey (rotateRight)) {
			rotateSpeed = Mathf.Min (rotateMaxSpeed, rotateSpeed + rotateAcceleration);
		} else {
			
			if(rotateSpeed > 0f) {
				rotateSpeed = Mathf.Max(0f, rotateSpeed - rotateAcceleration);
			} else {
				rotateSpeed = Mathf.Min(0f, rotateSpeed + rotateAcceleration);
			}
		}
		
		Vector3 rot = rotateSpeed * Vector3.forward;


		transform.Rotate(rot);

	}
}
