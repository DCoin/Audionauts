using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class GrooveFader : MonoBehaviour {

	Image im;
	GrooveManager gm;


	// Use this for initialization
	void Start () {

		im = this.GetComponent<Image>();
		gm = GrooveManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {


		Color c = im.color;
		c.a = 1f - gm.GroovePCT;
		im.color = c;
	
	}
}
