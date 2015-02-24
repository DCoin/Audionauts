using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

    public KeyCode rotateLeft;
    public KeyCode rotateRight;

    public float rotateSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 rot = rotateSpeed * Vector3.forward;

        if (Input.GetKey(rotateLeft))
        {
            transform.Rotate(-rot);
        }

        if (Input.GetKey(rotateRight))
        {
            transform.Rotate(rot);
        }

	}
}
