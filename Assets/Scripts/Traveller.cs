using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class Traveller : MonoBehaviour {

        public Vector3 LastPosition { get; private set; }

        public Vector3 CurrentPosition { get { return transform.position; } }

        private void Start()
        {
            LastPosition = CurrentPosition;

        }

        private void Update()
        {
            LastPosition = CurrentPosition;

            var pos = transform.localPosition;

            pos.z = MusicManager.Instance.BeatsPlayed;

            transform.localPosition = pos;
        }
    }
}
