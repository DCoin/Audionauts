using Assets.Scripts.CollisionHandlers;
using UnityEngine;

namespace Assets.Scripts {

    public class NoteHitAnimator : MonoBehaviour {

        public AnimationCurve SucessAnimation;
        public AnimationCurve FailureAnimation;

        public bool Success;

        public float Length = 1;

        private float time = 0f;

        private Vector3 localScale;

        private bool play = false;

        private void OnCollision(object sender, bool pre, bool success) {

            if (pre)
                return;

            play = true;
            Success = success;
        }

        // Use this for initialization
        void Start () {

            var note = (CollisionHandler) GetComponentInParent<Note>();

            note.Collision += OnCollision;

            localScale = transform.localScale;

        }
	
        // Update is called once per frame
        void Update () {

            if (!play)
                return;

            time += Time.deltaTime / Length;

            var animation = Success ? SucessAnimation : FailureAnimation;
	    
            var value = animation.Evaluate(time);

            transform.localScale = value*localScale;


        }
    }
}
