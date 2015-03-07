using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HamsterWheel : MonoBehaviour
{
	public Transform Player1;
	public Transform Player2;
	public static float g = 9.81f;
	public float PMass = 1;
	public float Mass = 1;
	public float Drag = 0.9f;
	//public float Friction = 0.5f;

	private float Radius = 17.5f;
	private float angularVelocity = 0;

	void Start ()
	{

	}

	void FixedUpdate ()
	{
		ApplyGravityFrom(Player1);
		ApplyGravityFrom(Player2);

		angularVelocity *= Drag;
		transform.Rotate(new Vector3(0, 0, angularVelocity * Time.deltaTime / Radius * 2)); // angularVelocity * Time.deltaTime / (radius * Mathf.PI) * 2 * Mathf.PI
	}

	void ApplyGravityFrom (Transform t)
	{
		var v = t.rotation * Vector3.up; // Vector where x and y is cos and sin of the angle
		var f = PMass * g * v.x;
		angularVelocity += f / Mass * Time.deltaTime;
	}
}
