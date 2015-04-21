using Assets.Scripts.CollisionHandlers;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts {

    public class NoteHitAnimator : MonoBehaviour {

        public AnimationCurve Animation;
        public AnimationCurve SucessAnimation;
        public AnimationCurve FailureAnimation;

        private MusicManager mm;


        private bool hitSuccess;

        public float Length = 1;

        private float time = 0f;

        private Vector3 localScale;

        private bool hit = false;

        private void OnCollision(object sender, bool pre, bool success) {

            if (pre)
                return;

            hit = true;
            hitSuccess = success;
        }

        // Use this for initialization
        void Start () {

            mm = MusicManager.Instance;


            var note = (CollisionHandler) GetComponentInParent<Note>();

            note.Collision += OnCollision;

            localScale = transform.localScale;

        }
	
        // Update is called once per frame
        void Update () {

            if (!hit) {

                var t = mm.SmoothBeatsPlayed;
                t -= (int) t;

                var v = Animation.Evaluate(t);

                transform.localScale = v*localScale;

            } else {

                time += Time.deltaTime/Length;

                var anim = hitSuccess ? SucessAnimation : FailureAnimation;

                var value = anim.Evaluate(time);

                transform.localScale = value*localScale;
            }


        }
    }
}
