using UnityEngine;
using System.Collections;

public class ColorController : MonoBehaviour {

	public Color color;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Renderer r = this.GetComponent<Renderer> ();

		r.material.color = this.color;

	
	}
}
