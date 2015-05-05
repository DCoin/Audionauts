using UnityEngine;
using System.Collections;

public class TailSpin : MonoBehaviour {
	
	public float Decay = 0.98f;
	public float tailModifier = 0.001f;
	public float minLength = 0.2f;
	private Vector2 lastPosition;
	private float moveMeter;
	private ParticleSystem system;

	void Start () {
		system = GetComponent<ParticleSystem>();
	}

	void FixedUpdate () {
		Vector2 pos = transform.position;
		moveMeter += (lastPosition - pos).sqrMagnitude;
		lastPosition = pos;
		moveMeter *= Decay;
		system.startLifetime = minLength + moveMeter * tailModifier;
	}
}
