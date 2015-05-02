using UnityEngine;
using System.Collections;

public class TailEmitter : MonoBehaviour {

	public Transform Player;
	public int EmissionRate = 1000;
	public float Radius = .1f;
	public float Speed = 40;
	//public bool interpolate;

	ParticleSystem _particleSystem;
	ParticleSystem.Particle[] particles;
	Vector3 lastPosition;

	Vector3 localPlayerPosition {
		get { return Player.localPosition; }
	}

	void Start () {
		_particleSystem = GetComponent<ParticleSystem>();
		particles = new ParticleSystem.Particle[_particleSystem.maxParticles];
		lastPosition = Player.position;
	}

	void LateUpdate() {
		var emitCount = EmissionRate * Time.deltaTime;
		for (int i = 0; i < emitCount; i++) {
			var velocity = Vector3.forward * -Speed;
			var fraction = i/emitCount;
			/*if (interpolate)*/ _particleSystem.Emit(Vector3.Lerp(localPlayerPosition, lastPosition + velocity * Time.deltaTime, fraction) + (Vector3)(Radius * Random.insideUnitCircle),
			                                          velocity,
			                                          _particleSystem.startSize,
			                                          Mathf.Lerp(_particleSystem.startLifetime, _particleSystem.startLifetime + Time.deltaTime, fraction),
			                                          _particleSystem.startColor);
			//else _particleSystem.Emit(Vector3.Lerp(lastPosition, Player.position, ((float)i)/EmissionRate), Vector3.zero, _particleSystem.startSize, _particleSystem.startLifetime, _particleSystem.startColor);
		}
		lastPosition = localPlayerPosition;
	}
}
