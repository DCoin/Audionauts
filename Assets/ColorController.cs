using UnityEngine;

public class ColorController : MonoBehaviour {

	public Color color;

	
	// Update is called once per frame
	void Update () {

		UpdateColor ();
	
	}

	void OnValidate() {
		
		UpdateColor ();
		
	}

	void UpdateColor() {

		Renderer r = this.GetComponent<Renderer> ();

		if (r != null && r.sharedMaterial != null) {
		
			r.sharedMaterial.color = this.color;

		}
	}
}
