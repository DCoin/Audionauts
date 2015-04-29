using UnityEngine;
using System.Collections;

public class TailSpin : MonoBehaviour {
	
	public float Decay = 0.98f;
	public float tailModifier = 0.001f;
	public float minLength = 0.2f;
	private Vector2 lastPosition;
	private float moveMeter;
	private new ParticleSystem particleSystem;

	void Start () {
		particleSystem = GetComponent<ParticleSystem>();
	}

	void FixedUpdate () {
		Vector2 pos = transform.position;
		moveMeter += (lastPosition - pos).sqrMagnitude;
		lastPosition = pos;
		moveMeter *= Decay;
		particleSystem.startLifetime = minLength + moveMeter * tailModifier;
	}
}
