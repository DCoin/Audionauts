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

            var gm = GrooveManager.Instance;

            var value = Animation.Evaluate(time);

            if (gm != null) {
                
                value = Mathf.Lerp(1, value, GrooveManager.Instance.PercentGroove);

            }

            transform.localScale = value*localScale;

        }

    }
}
