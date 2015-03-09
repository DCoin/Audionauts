using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HamsterPhysic : MonoBehaviour
{
	public float g = 1000f;
	public float Mass = 1;
	public float Drag = 0.9f;
	
	protected float Radius = 17.5f;
	private float _angularVelocity = 0;
	private float _angularVelocityChange = 0;
	public float angularVelocity
	{
		get {
			return _angularVelocity;
		}
		set {
			_angularVelocityChange +=  value - _angularVelocity;
		}
	}
	
	public virtual void FixedUpdate ()
	{
		_angularVelocity += _angularVelocityChange;
		_angularVelocityChange = 0;
		transform.Rotate(new Vector3(0, 0, angularVelocity * Time.deltaTime / Radius * 2)); // angularVelocity * Time.deltaTime / (radius * Mathf.PI) * 2 * Mathf.PI
		_angularVelocity *= Drag;
	}
}
