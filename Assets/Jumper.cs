using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {

    public Transform target;

    public float jumpAfter = 100f;

    public float barJump = 4f;
	
	// Update is called once per frame
	void Update () {

		float z0 = target.position.z;
		float z1 = this.transform.position.z;
		float dz = z1 - z0; 
        
        if (dz <= -jumpAfter)
        {
            Vector3 v = this.transform.localPosition;
            v.z += barJump;
            this.transform.localPosition = v;
        }
	
	}
}
