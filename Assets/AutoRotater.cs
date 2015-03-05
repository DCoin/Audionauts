using UnityEngine;
using System.Collections;

public class AutoRotater : MonoBehaviour {

	public enum Direction { Left, Right }

	public Direction rotationDirection;
	public float rotateAcceleration;
	public float rotateMaxSpeed = 3;
	
	private float rotateSpeed = 0;
	
	// Update is called once per frame
	void Update ()
	{

		switch (rotationDirection) {
		case Direction.Left: {
			rotateSpeed = Mathf.Max(-rotateMaxSpeed, rotateSpeed - rotateAcceleration);
		} break;

		case Direction.Right: {
			rotateSpeed = Mathf.Min (rotateMaxSpeed, rotateSpeed + rotateAcceleration);
		} break;
		default: {
			if(rotateSpeed > 0f) {
				rotateSpeed = Mathf.Max(0f, rotateSpeed - rotateAcceleration);
			} else {
				rotateSpeed = Mathf.Min(0f, rotateSpeed + rotateAcceleration);
			}
		} break;
		}

		Vector3 rot = rotateSpeed * Vector3.forward;
		
		
		transform.Rotate(rot);

	}

}
