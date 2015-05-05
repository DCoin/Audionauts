using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class Traveller : MonoBehaviour {

		// The distance the traveller moves per beat
		public const float BeatScaling = .25f;

		/*
        void OnGUI() {

            GUI.Label(new Rect(32, 32, 128, 128), transform.localPosition.z.ToString());

        }
		*/

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

            pos.z = MusicManager.Instance.SmoothBeatsPlayed * BeatScaling;

            transform.localPosition = pos;
        }
    }
}
