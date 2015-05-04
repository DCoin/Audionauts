using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts {

    [RequireComponent(typeof(CircleRenderer))]
    public class CreditMarker : MonoBehaviour {

        public AnimationCurve fadeCurve;

        private Transform target;

        private float radius;

        public float Limit;

        private CircleRenderer circle;

        private Color circleColor;

        public float Scale;

        public float Alpha;

        public SoundBite SoundBite;

        // Use this for initialization
        void Start() {

            circle = GetComponent<CircleRenderer>();
            radius = circle.Radius;
            target = StageManager.Instance.Traveller.transform;
            circleColor = circle.Color;

        }

        // Update is called once per frame
        void LateUpdate() {

            var sz = StageManager.Instance.Stage.localScale.z;

            var dz = (transform.position.z - target.transform.position.z) / sz;

            if(dz > Limit || dz < 0) {
                circle.Color = Color.clear;
            } else {
                var color = circleColor;

                var p = (1f - dz / Limit);

                color.a = Alpha * fadeCurve.Evaluate(p);
                circle.Color = color;
            }

            dz *= Scale;

            circle.Radius = radius + Mathf.Abs(dz);
            circle.Refresh();

            CheckCollision();

        }

        void CheckCollision() {

            

            var trv = StageManager.Instance.Traveller;
            var za = trv.LastPosition.z;
            var zb = trv.CurrentPosition.z;
            var z = transform.position.z;

            if (za < z && z < zb) {

                var p1 = StageManager.Instance.Player1;
                var p2 = StageManager.Instance.Player2;

                if (Hits(p1.transform) || Hits(p2.transform)) {
                    SoundBite.Play();
                }

            }

        }

        bool Hits(Transform t) {

            Vector2 a = t.position;
            Vector2 b = transform.position;
            var r = circle.Radius;

            return (a - b).sqrMagnitude < r*r;

        }
    }
}
