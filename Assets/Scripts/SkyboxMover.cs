using UnityEngine;
using System.Collections;

public class SkyboxMover : MonoBehaviour {

	void FixedUpdate () {
		transform.Rotate(.04f,.04f,.01f);
	}
}
