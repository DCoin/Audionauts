using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteInitializer : MonoBehaviour {

        // Use this for initialization
        void Start () {

            var n = transform.GetComponentInParent<Note>();
            var r = GetComponent<SpriteRenderer>();
            r.color = n.CurrentColor;
        }
	
    }
}
