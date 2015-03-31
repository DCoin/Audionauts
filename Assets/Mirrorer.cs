using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Mirrorer : MonoBehaviour {

	public Transform source;


	public float angle;

	// Update is called once per frame
	void LateUpdate () {

		var v = source.transform.position;
		v = Quaternion.AngleAxis(angle, Vector3.forward) * v;

		//v.x *= -1f;
		//v.y *= -1f;
		transform.position = v;
	
	}
}
