using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {

    public Transform target;

    public float jumpAfter = 10f;

    public float jumpLength = 20f;
	
	// Update is called once per frame
	void Update () {

        float z0 = target.position.z;
        float z1 = this.transform.position.z;
        float dz = z1 - z0; 

        if (dz <= -jumpAfter)
        {
            Vector3 v = this.transform.position;
            v.z += jumpLength;
            this.transform.position = v;
        }
	
	}
}
