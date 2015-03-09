using UnityEngine;
using System.Collections;

public class Shrinker : MonoBehaviour {

	public Health head;

	private Vector3 initialScale;

	public float maxLength;

	// Use this for initialization
	void Start () {

		initialScale = transform.localScale;
	
	}
	
	// Update is called once per frame
	void Update () {

		var a = transform.position;
		var b = head.transform.position;
		float c = maxLength * head.CurrentHealthPct;

		float d = (a - b).magnitude;

		if (d > maxLength) {
			Destroy (this.gameObject);
		} else if(d > c) {
			transform.localScale = Vector3.zero;	
		} else {
			transform.localScale = initialScale * (1f-d/c);
		}
	
	}
}
