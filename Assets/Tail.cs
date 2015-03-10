using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class Tail : MonoBehaviour {

    public float interval;

    private float timeSinceLastDrop;

    public Shrinker tailPrefab;

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

            Shrinker g = (Shrinker) Instantiate(tailPrefab, this.transform.position, this.transform.rotation);
            g.transform.parent = tailsParent;
			g.head = this.GetComponent<Health>();
			g.enabled = true;

			UniformScaler ush = this.GetComponent<UniformScaler>();
			UniformScaler usb = g.GetComponent<UniformScaler>();
			if(usb != null) {
				usb.scale = ush.scale;
			}

			Renderer ra = this.gameObject.GetComponent<Renderer>();
			Renderer rb = g.GetComponent<Renderer>();

			rb.material.color = ra.material.color;
        }
	
	}
}
