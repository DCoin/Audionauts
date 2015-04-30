using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleColorController : MonoBehaviour {

    private ParticleSystem ps;

    public Color color;

	// Use this for initialization
	void Start () {

        ps = GetComponent<ParticleSystem>();

        ps.startColor = color;

	}

    void OnValidate() {

        ps = GetComponent<ParticleSystem>();

        ps.startColor = color;
        
    }
}
