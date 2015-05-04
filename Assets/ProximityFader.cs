using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleRenderer))]
public class ProximityFader : MonoBehaviour {

    public Transform Target;

    public float MaxDistance;

    private CircleRenderer circle;

	// Use this for initialization
	void Start () {

        circle = GetComponent<CircleRenderer>();
	
	}
	
	// Update is called once per frame
	void Update () {

        var color = circle.Color;

        var a = transform.position.z;
        var b = Target.position.z;

        var c = Mathf.Abs(a - b);

        if(c > MaxDistance) {

            c = MaxDistance;

        }

	    c /= MaxDistance;

        color.a = 1f - c;

        circle.Color = color;

        circle.Refresh();

	    var text = GetComponentInChildren<TextMesh>();

	    if (text == null)
	        return;

	    text.color = color;


	}
}
