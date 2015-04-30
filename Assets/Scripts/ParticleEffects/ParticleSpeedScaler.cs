using UnityEngine;
using System.Collections;
using Assets.Scripts.Managers;

[RequireComponent (typeof(ParticleSystem))]
public class ParticleSpeedScaler : MonoBehaviour {
	
	public float SpeedRangeMin = 10, SpeedRangeMax = 70;
	public float MinSpeedScale = .5f, MaxSpeedScale = 2;
	public float MaxSize = .7f;
	public AnimationCurve SizeBySpeed;
	public float MaxLength = 5;
	public AnimationCurve LengthByGroove;
	
	private new ParticleSystem particleSystem;
	private ParticleSystemRenderer particleSystemRenderer;
	private ParticleSystem.Particle[] particles;
	private float currentGrooveScale {
		get { return MinSpeedScale + (MaxSpeedScale - MinSpeedScale) * GrooveManager.Instance.PercentGroove; }
	}
//	private float startSpeed;
//	private float minSpeed;
//	private float speedScale;

	void Start () {
		particleSystem = GetComponent<ParticleSystem>();
		particles = new ParticleSystem.Particle[particleSystem.maxParticles];
		particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
//		startSpeed = particleSystem.startSpeed;
//		minSpeed = particleSystem.startSpeed * MinSpeedScale;
//		speedScale = particleSystem.startSpeed * (MaxSpeedScale - MinSpeedScale);
	}

	void LateUpdate() {
		var alive = particleSystem.GetParticles(particles);
		var speedRange = SpeedRangeMax - SpeedRangeMin;
		var grooveScale = currentGrooveScale;

		particleSystemRenderer.lengthScale = LengthByGroove.Evaluate(grooveScale) * MaxLength;

		for (int i = 0; i < alive; i++) {
			var variation = particles[i].rotation;
			particles[i].size = SizeBySpeed.Evaluate(variation) * MaxSize; // This only needs to be done once for new particles but i do not know how to detect that.
			particles[i].velocity = (Vector3.forward * (SpeedRangeMin + speedRange * variation)) * grooveScale;
			// TODO Remove particles when groove is going down??
		}

		particleSystem.SetParticles(particles, alive);
	}
}
