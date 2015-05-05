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
	
	private ParticleSystem system;
	private ParticleSystemRenderer particleSystemRenderer;
	private ParticleSystem.Particle[] particles;
	private float currentGrooveScale {
		get { return MinSpeedScale + (MaxSpeedScale - MinSpeedScale) * GrooveManager.Instance.PercentGroove; }
	}

	void Start () {
		system = GetComponent<ParticleSystem>();
		particles = new ParticleSystem.Particle[system.maxParticles];
		particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
	}

	void LateUpdate() {
		var alive = system.GetParticles(particles);
		var speedRange = SpeedRangeMax - SpeedRangeMin;
		var grooveScale = currentGrooveScale;

		particleSystemRenderer.lengthScale = LengthByGroove.Evaluate(grooveScale) * MaxLength;

		for (int i = 0; i < alive; i++) {
			var variation = particles[i].rotation;
			particles[i].size = SizeBySpeed.Evaluate(variation) * MaxSize; // This only needs to be done once for new particles but i do not know how to detect that.
			particles[i].velocity = (Vector3.forward * (SpeedRangeMin + speedRange * variation)) * grooveScale;
			// TODO Remove particles when groove is going down??
		}

		system.SetParticles(particles, alive);
	}
}
