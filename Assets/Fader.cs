using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Fader : MonoBehaviour {

    public Transform target;

    private SpriteRenderer sr;

    public float power;

	// Use this for initialization
	void Start () {

        sr = this.GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {

        float z0 = target.position.z;
        float z1 = this.transform.position.z;
        float dz = z1 - z0; 

        Color col = sr.color;
        col.a = power / Mathf.Abs(dz);
        sr.color = col;
	
	}
}
