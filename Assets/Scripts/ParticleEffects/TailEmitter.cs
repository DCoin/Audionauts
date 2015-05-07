using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Managers;

public class TailEmitter : MonoBehaviour {

	public Transform Player;
	public int EmissionRate = 1000;
	public float Radius = .1f;
	public float Speed = 40;
    public float Rotation = 0f;

	public AnimationCurve NoteAcceleration;
	public float AccelerationTime = .5f;
	public float MinAcceleration = 0;
	public float MaxAcceleration = 1;

	private float lastHit = 2;

    private float totalRot = 0f;
	//public bool interpolate;

	ParticleSystem _particleSystem;
	ParticleSystem.Particle[] pParticles;
	Vector3 lastPosition;

    private Transform playerModel;

	Vector3 localPlayerPosition {
		get { return Player.localPosition; }
	}

	void Start () {

		_particleSystem = GetComponent<ParticleSystem>();
		pParticles = new ParticleSystem.Particle[_particleSystem.maxParticles];
		lastPosition = Player.position;

	    PlayerBobber b = Player.GetComponentInChildren<PlayerBobber>();
	    playerModel = b.transform;

	}

	public void Hit() {
		lastHit = 0;
	}

	void LateUpdate() {
		
		var _acceleration = 0f;
		if (lastHit < 1) {
			_acceleration = (NoteAcceleration.Evaluate(lastHit)) * Speed;
			lastHit += Time.deltaTime / AccelerationTime;
		}
		float grooveMult;
		if (GrooveManager.Instance == null) grooveMult = 0;
		else grooveMult = Mathf.Lerp(MinAcceleration, MaxAcceleration, GrooveManager.Instance.PercentGroove);
		var pVelocity = Vector3.forward * -(Speed + _acceleration * grooveMult);


		var emitCount = EmissionRate * Time.deltaTime;
		for (int i = 0; i < emitCount; i++) {
			var fraction = i/emitCount;

		    //var part = new ParticleSystem.Particle();
		    var pPosition = Vector3.Lerp(localPlayerPosition, lastPosition + pVelocity*Time.deltaTime, fraction) +
		                    (Vector3) (Radius*Random.insideUnitCircle);
		    var pSize = _particleSystem.startSize + playerModel.localScale.y;
		    var pLifeTime = Mathf.Lerp(
		        _particleSystem.startLifetime,
		        _particleSystem.startLifetime + Time.deltaTime,
		        fraction);
		    var pColor = _particleSystem.startColor;

			/*if (interpolate)*/
		    _particleSystem.Emit(pPosition, pVelocity, pSize, pLifeTime, pColor);
			//else _particleSystem.Emit(Vector3.Lerp(lastPosition, Player.position, ((float)i)/EmissionRate), Vector3.zero, _particleSystem.startSize, _particleSystem.startLifetime, _particleSystem.startColor);
		}
		lastPosition = localPlayerPosition;

		int pCount = _particleSystem.GetParticles(pParticles);

	    totalRot += Rotation * Mathf.PI * 2f;

	    for (int i = 0; i < pCount; ++i) {

	        if (pParticles[i].rotation == 0f) {
	            pParticles[i].rotation = totalRot;
	        }

            //pParticles[i].rotation += Rotation;

			pParticles[i].velocity = pVelocity;

	    }

        _particleSystem.SetParticles(pParticles, pCount);
	}
}
