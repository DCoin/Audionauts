using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleHomer : MonoBehaviour {

    public Transform Target;
    
    public AnimationCurve Curve;

    public float Distance = 0.5f;

    public float Force = 20f;

    public float Duration = 5f;

    private ParticleSystem system;
    
    private ParticleSystem.Particle[] particles;
    
    private float time;

	// Use this for initialization
	void Start () {

	    system = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[system.maxParticles];
	    time = 0f;


	}
	
	// Update is called once per frame
	void LateUpdate () {

	    time += Time.deltaTime;

	    int particleCount = system.GetParticles(particles);

        var to = Target.position;
	    var sqDist = Distance*Distance;

	    for (int i = 0; i < particleCount; ++i) {

	        var progress = Mathf.Clamp01(time/Duration);

	        var from = particles[i].position;

	        var direction = to - from;

	        if (direction.sqrMagnitude < sqDist) {

	            particles[i].lifetime = 0f;
	            continue;
	        }

	        direction.Normalize();

	        particles[i].velocity = Vector3.Lerp(particles[i].velocity, direction*Force, Curve.Evaluate(progress));

	        //particles[i].velocity *= 0.9f;
	        //particles[i].velocity += f * Force;

	    }

        system.SetParticles(particles, particleCount);





	}
}
