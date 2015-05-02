using UnityEngine;
using System.Collections;
using Assets.Scripts.Managers;

public class SpeedScaler : MonoBehaviour {

	public float MaxScale = 200;
	public float MinScale = 100;

	void Start () {
	
	}

	void Update () {
		transform.localScale = new Vector3(1, 1, MinScale + (MaxScale - MinScale) * GrooveManager.Instance.PercentGroove);
	}
}
