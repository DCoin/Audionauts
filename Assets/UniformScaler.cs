using UnityEngine;
using System.Collections;

public class UniformScaler : MonoBehaviour {

	public float scale = 1f;
	private Vector3 initialScale;

	// Use this for initialization
	void Start () {

		initialScale = transform.localScale;

	}
	
	// Update is called once per frame
	void Update () {
	
		transform.localScale = initialScale * scale;

	}
}
