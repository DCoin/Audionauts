using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts {

    [RequireComponent(typeof(CircleRenderer))]
    public class NoteMarker : MonoBehaviour {

        public AnimationCurve fadeCurve;

        private Transform target;

        private float radius;

        public float Limit;

        private CircleRenderer circle;

        private Color circleColor;

        public float Scale;

        public float Alpha;

        // Use this for initialization
        void Start() {

            circle = GetComponent<CircleRenderer>();
            radius = circle.Radius;
            target = StageManager.Instance.Traveller.transform;
            circleColor = circle.Color;

            var note = GetComponentInParent<Note>();
            circleColor = note.CurrentColor;
            

        }

        // Update is called once per frame
        void LateUpdate() {

            var sz = StageManager.Instance.Stage.localScale.z;

            var dz = (transform.position.z - target.transform.position.z) / sz;

            if (dz > Limit || dz < 0) {
                circle.Color = Color.clear;
            } else {
                var color = circleColor;

                var p = (1f - dz/Limit);

                color.a = Alpha * fadeCurve.Evaluate(p);
                circle.Color = color;
            }

            dz *= Scale;

            circle.Radius = radius + Mathf.Abs(dz);
            circle.Refresh();
        }
    }
}
