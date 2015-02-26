using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

    public KeyCode rotateLeft;
    public KeyCode rotateRight;

    public float rotateSpeed;
	
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
