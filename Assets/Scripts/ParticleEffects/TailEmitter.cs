using UnityEngine;
using Assets.Scripts;

public class TailEmitter : MonoBehaviour {

	public Transform Player;
	public int EmissionRate = 1000;
	public float Radius = .1f;
	public float Speed = 40;
    public float Rotation = 0f;
    private float totalRot = 0f;
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
			var pVelocity = Vector3.forward * -Speed;
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

	    int pCount = _particleSystem.particleCount;
        ParticleSystem.Particle[] pParticles = new ParticleSystem.Particle[pCount];


	    _particleSystem.GetParticles(pParticles);

	    totalRot += Rotation * Mathf.PI * 2f;

	    for (int i = 0; i < pCount; ++i) {

	        if (pParticles[i].rotation == 0f) {
	            pParticles[i].rotation = totalRot;
	        }

            //pParticles[i].rotation += Rotation;

	    }

        _particleSystem.SetParticles(pParticles, pCount);
	}
}
