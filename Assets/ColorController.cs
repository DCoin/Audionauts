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
		
		r.sharedMaterial.color = this.color;

	}
}
