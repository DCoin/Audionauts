using Assets.Scripts.CollisionHandlers;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Assets.Scripts {

    [RequireComponent(typeof(NoteCollisionHandler))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Note : MonoBehaviour {
        private static CollisionHandler Player1 {
            get { return PlayerManager.Instance.Player1; }
        }

        private static CollisionHandler Player2 {
            get { return PlayerManager.Instance.Player1; }
        }

        private NoteCollisionHandler Handler { get { return GetComponent<NoteCollisionHandler>(); } }

        private Color CurrentColor {
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

        public void RefreshColor() {

            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = CurrentColor;

        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected() {
            var stdColor = Handles.color;

            Handles.color = CurrentColor;

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

        public void ResolveCollisions() {
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
                    OnCollisionFirst();
                    break;
                // Only second player.
                case 2:
                    OnCollisionSecond();
                    break;
                // Both players.
                case 3:
                    OnCollisionBoth();
                    break;
                // This case should not happen.
                default:
                    OnCollisionError();
                    break;
            }

        }

        private void OnCollisionFirst() {
            switch(_kind) {
                case NoteKind.Any:
                case NoteKind.First:
                    Player1.OnCollision();
                    Handler.OnCollision();
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

        private void OnCollisionSecond() {
            switch(_kind) {
                case NoteKind.Any:
                case NoteKind.Second:
                    Player2.OnCollision();
                    Handler.OnCollision();
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

        private void OnCollisionBoth() {
            switch(_kind) {
                case NoteKind.Second:
                    Player2.OnCollision();
                    Handler.OnCollision();
                    break;
                case NoteKind.First:
                    Player2.OnCollision();
                    Handler.OnCollision();
                    break;
                case NoteKind.Any:
                case NoteKind.Both:
                    Player1.OnCollision();
                    Player2.OnCollision();
                    Handler.OnCollision();
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
