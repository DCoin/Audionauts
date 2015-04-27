using UnityEngine;
using System.Collections;

public class AnglePlacer : MonoBehaviour {

    public float Radius = 10f;
    public float Angle;

    void OnValidate() {

        var rad = (90f - Angle)*Mathf.Deg2Rad;
        
        var x = Mathf.Cos(rad);
        var y = Mathf.Sin(rad);

        var pos = new Vector2(x,y);

        transform.localPosition = pos * Radius;

    }
}
