using UnityEngine;
using System.Collections;

public class DestroyOutside : MonoBehaviour {
	

	private Renderer renderer;

	private bool seen = false;

	void Start() {

		renderer = GetComponent<Renderer> ();

	}

	void Update() {

		if (renderer.isVisible)
			seen = true;
		
		if (seen && !renderer.isVisible)
			Destroy(gameObject);
	}
}
