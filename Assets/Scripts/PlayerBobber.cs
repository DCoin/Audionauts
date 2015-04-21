using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts {
    public class PlayerBobber : MonoBehaviour {

        public AnimationCurve Animation;

        private MusicManager mm;

        private Vector3 localScale;

        // Use this for initialization
        void Start () {
	
            mm = MusicManager.Instance;

            localScale = transform.localScale;

        }
	
        // Update is called once per frame
        void Update () {

            var time = mm.SmoothBeatsPlayed;
            time -= (int) time;

            var value = Animation.Evaluate(time);

            transform.localScale = value*localScale;

        }

    }
}
