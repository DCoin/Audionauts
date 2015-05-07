using UnityEngine;
using System.Collections;
using Assets.Scripts;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleColorController : MonoBehaviour {

	// Use this for initialization
	void Start () {

        var ps = GetComponent<ParticleSystem>();

	    var nt = GetComponentInParent<Note>();

	    ps.startColor = nt.CurrentColor;

	}

}
