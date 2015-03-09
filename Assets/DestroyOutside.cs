using UnityEngine;
using System.Collections;

public class DestroyOutside : MonoBehaviour {
	

	private Renderer r;

	private bool seen = false;

	void Start() {

		r = GetComponent<Renderer> ();

	}

	void Update() {

		if (r.isVisible)
			seen = true;
		
		if (seen && !r.isVisible)
			Destroy(gameObject);
	}
}
