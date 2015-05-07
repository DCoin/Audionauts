using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteInitializer : MonoBehaviour {

        public bool customAlpha = false;
        public float alpha;

        // Use this for initialization
        void Start () {

            var n = transform.GetComponentInParent<Note>();
            var r = GetComponent<SpriteRenderer>();
            var c = n.CurrentColor;
            if(customAlpha)
                c.a = alpha;
            r.color = c;

        }
	
    }
}
