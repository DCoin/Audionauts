using UnityEngine;
using System.Collections;

public class Radius : MonoBehaviour {

    public float value;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.Lerp(Color.clear, Color.green, 0.8f);
        Gizmos.DrawSphere(transform.position, value);
    }
}
