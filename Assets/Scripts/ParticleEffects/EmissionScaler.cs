using UnityEngine;
using System.Collections;
using Assets.Scripts.Managers;

public class EmissionScaler : MonoBehaviour {
	
	public float MinEmissionRate = 0;
	public float MaxEmissionRate = 60;

	private new ParticleSystem particleSystem;

	void Start () {
		particleSystem = GetComponent<ParticleSystem>();
	}

	void Update () {
		particleSystem.emissionRate = MinEmissionRate + (MaxEmissionRate - MinEmissionRate) * GrooveManager.Instance.PercentGroove;
	}
}
