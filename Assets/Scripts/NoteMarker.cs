using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts {

    [RequireComponent(typeof(CircleRenderer))]
    public class NoteMarker : MonoBehaviour {

        private Transform target;

        private float radius;

        private CircleRenderer circle;

        // Use this for initialization
        void Start() {

            circle = GetComponent<CircleRenderer>();
            radius = circle.Radius;
            target = StageManager.Instance.Traveller.transform;

        }

        // Update is called once per frame
        void LateUpdate() {

            var dz = transform.position.z - target.transform.position.z;

            circle.Radius = radius + Mathf.Abs(dz);
            circle.Refresh();
        }
    }
}
