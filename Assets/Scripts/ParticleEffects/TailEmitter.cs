using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class TailEmitter : MonoBehaviour {

	public Transform Player;
	public int EmissionRate = 1000;
	public float Radius = .1f;
	public float Speed = 40;
	//public bool interpolate;

	ParticleSystem _particleSystem;
	Vector3 lastPosition;

    private Transform playerModel;

	Vector3 localPlayerPosition {
		get { return Player.localPosition; }
	}

	void Start () {
		_particleSystem = GetComponent<ParticleSystem>();
		lastPosition = Player.position;

	    PlayerBobber b = Player.GetComponentInChildren<PlayerBobber>();
	    playerModel = b.transform;

	}

	void LateUpdate() {

		var emitCount = EmissionRate * Time.deltaTime;
		for (int i = 0; i < emitCount; i++) {
			var velocity = Vector3.forward * -Speed;
			var fraction = i/emitCount;
			/*if (interpolate)*/ _particleSystem.Emit(Vector3.Lerp(localPlayerPosition, lastPosition + velocity * Time.deltaTime, fraction) + (Vector3)(Radius * Random.insideUnitCircle),
			                                          velocity,
			                                          _particleSystem.startSize + playerModel.localScale.y,
			                                          Mathf.Lerp(_particleSystem.startLifetime, _particleSystem.startLifetime + Time.deltaTime, fraction),
			                                          _particleSystem.startColor);
			//else _particleSystem.Emit(Vector3.Lerp(lastPosition, Player.position, ((float)i)/EmissionRate), Vector3.zero, _particleSystem.startSize, _particleSystem.startLifetime, _particleSystem.startColor);
		}
		lastPosition = localPlayerPosition;
	}
}
