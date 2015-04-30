using UnityEngine;
using System.Collections;
using Assets.Scripts.Managers;

public class SpeedScaler : MonoBehaviour {

	public float Scale = 100;
	public float MinScale = 100;

	void Start () {
	
	}

	void Update () {
		transform.localScale = new Vector3(1, 1, MinScale + Scale * GrooveManager.Instance.PercentGroove);
	}
}
