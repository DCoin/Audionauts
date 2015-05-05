using UnityEngine;
using Assets.Scripts.Managers;

public class EmissionScaler : MonoBehaviour {
	
	public float MinEmissionRate = 0;
	public float MaxEmissionRate = 60;

	private ParticleSystem system;

	void Start () {
		system = GetComponent<ParticleSystem>();
	}

	void Update () {
		system.emissionRate = MinEmissionRate + (MaxEmissionRate - MinEmissionRate) * GrooveManager.Instance.PercentGroove;
	}
}
