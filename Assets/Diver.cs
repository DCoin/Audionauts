using UnityEngine;
using System.Collections;

public class Diver : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position;

        pos.z += speed * Time.deltaTime;

        transform.position = pos;
	
	}
}
