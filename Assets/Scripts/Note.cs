using Assets.Scripts.CollisionHandlers;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Assets.Scripts {

    [RequireComponent(typeof(SpriteRenderer))]
    public class Note : CollisionHandler {
        // Is used to store an audiosource if it has been prepared
        private AudioSource playing;

        public int Index = -1;

        private static CollisionHandler Player1 {
            get { return StageManager.Instance.Player1; }
        }

        private static CollisionHandler Player2 {
            get { return StageManager.Instance.Player2; }
        }

        public SoundBite SoundBite;

        public override void OnCollision(bool pre) {
            //Debug.Log("SongPosition: " + SongPosition + " BeatsPlayed: " + MusicManager.Instance.BeatsPlayed);
            if (pre && playing == null) playing = SoundBite.Play(BeatPosition, true);
            else if (playing == null) playing = SoundBite.Play(BeatPosition);

			if (!pre) GrooveManager.Instance.Groove += 1;

            base.OnCollision(pre);
        }

//        public void Update() {
//            if (playing != null) Debug.Log ("Shouldhaplayed: " + (MusicManager.Instance.BeatsPlayed - SongPosition) + " HasPlayed: " + playing.time);
//        }

        public Color CurrentColor {
            get {
                var section = GetComponentInParent<Section>();
                return section.PalettePrefab.GetColor(Kind);
            }
        }

        public float LocalRadius;

        public float GlobalRadius {
            get {
                var s = transform.lossyScale;
                return LocalRadius * (s.x + s.y) / 2f;

            }
        }

        [SerializeField]
        [HideInInspector]
        private NoteKind _kind;

        public NoteKind Kind {

            get { return _kind; }

            set {
                _kind = value;
                RefreshColor();
            }

        }

        private float BeatPosition {
            get { return transform.position.z / StageManager.Instance.Stage.localScale.z / Traveller.BeatScaling; }
        }

        public void RefreshColor() {

            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = CurrentColor;

        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected() {
            var stdColor = Handles.color;

            Handles.color = Color.Lerp(Color.white, Color.clear, 0.5f);

            Handles.DrawSolidArc(transform.parent.position, transform.forward, transform.localPosition, 5f, transform.localPosition.magnitude);
            Handles.DrawSolidArc(transform.parent.position, transform.forward, transform.localPosition, -5f, transform.localPosition.magnitude);


            Handles.DrawSolidDisc(transform.position, transform.forward, GlobalRadius);


            Handles.color = stdColor;

        }
#endif

        private bool CollidesWith(Transform subject) {
            Vector2 a = transform.position;
            Vector2 b = subject.position;

            var r = GlobalRadius;
            var rSqr = r * r;

            var dSqr = (a - b).SqrMagnitude();

            return dSqr < rSqr;

        }

        public void ResolveCollisions(bool pre) {
            if(_kind == NoteKind.None)
                return;

            var c = 0;

            if(CollidesWith(Player1.transform))
                c += 1;

            if(CollidesWith(Player2.transform))
                c += 2;

            switch(c) {
                // No hits.
                case 0:
                    // Do nothing.
                    break;
                // Only first player.
                case 1:
                    OnCollisionFirst(pre);
                    break;
                // Only second player.
                case 2:
                    OnCollisionSecond(pre);
                    break;
                // Both players.
                case 3:
                    OnCollisionBoth(pre);
                    break;
                // This case should not happen.
                default:
                    OnCollisionError();
                    break;
            }

        }

        private void OnCollisionFirst(bool pre)
        {
            switch(_kind) {
                case NoteKind.Any:
                case NoteKind.First:
                    Player1.OnCollision(pre);
                    OnCollision(pre);
                    break;
                case NoteKind.Second:
                case NoteKind.Both:
                    // Do nothing.
                    break;
                default:
                    OnCollisionError();
                    break;
            }
        }

        private void OnCollisionSecond(bool pre)
        {
            switch(_kind) {
                case NoteKind.Any:
                case NoteKind.Second:
                    Player2.OnCollision(pre);
                    OnCollision(pre);
                    break;
                case NoteKind.First:
                case NoteKind.Both:
                    // Do nothing.
                    break;
                default:
                    OnCollisionError();
                    break;
            }

        }

        private void OnCollisionBoth(bool pre) {
            switch(_kind) {
                case NoteKind.Second:
                    Player2.OnCollision(pre);
                    OnCollision(pre);
                    break;
                case NoteKind.First:
                    Player2.OnCollision(pre);
                    OnCollision(pre);
                    break;
                case NoteKind.Any:
                case NoteKind.Both:
                    Player1.OnCollision(pre);
                    Player2.OnCollision(pre);
                    OnCollision(pre);
                    break;
                default:
                    OnCollisionError();
                    break;
            }

        }

        private static void OnCollisionError() {
            Debug.LogError("Unexpected result in collision detection.");
        }

    }
}
