using UnityEngine;
using System.Collections;

public class Tail : MonoBehaviour {

    public float interval;

    private float timeSinceLastDrop;

    public GameObject tailPrefab;

    public Transform tailsParent; 

	// Use this for initialization
	void Start () {

        timeSinceLastDrop = interval;
	
	}
	
	// Update is called once per frame
	void Update () {

        timeSinceLastDrop += Time.deltaTime;

        if (timeSinceLastDrop > interval)
        {
            timeSinceLastDrop += interval;

            GameObject g = (GameObject) Instantiate(tailPrefab, this.transform.position, this.transform.rotation);
            g.transform.parent = tailsParent;
        }
	
	}
}
