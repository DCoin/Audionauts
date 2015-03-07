using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Looker : MonoBehaviour {

	public Transform target;

	// Update is called once per frame
	void LateUpdate () {
	
		this.transform.LookAt (target, Vector3.up);

	}
}
