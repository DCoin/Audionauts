using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Scripts {

    [RequireComponent(typeof(SpriteRenderer))]
    public class Note : MonoBehaviour
    {
        public delegate void CollisionEventHandler(object sender);

        public event CollisionEventHandler Collision;

        public Section Section
        {
            get
            {
                return GetComponentInParent<Section>();

            }
        }

        public AudioClip Clip
        {
            get { return Section.Clips[transform.GetSiblingIndex()]; }
        }

        public float LocalRadius;

        public float GlobalRadius
        {
            get
            {

                var s = transform.lossyScale;

                return LocalRadius * (s.x + s.y) / 2f;

            }
        }

        private static PlayerManager PlayerManager { get { return PlayerManager.Instance; } }

        [SerializeField]
		[HideInInspector]
        private NoteKind _kind;

        public NoteKind Kind
        {

            get { return _kind; }

            set
            {
                _kind = value;
                RefreshColor();
            }

        }

        private Color CurrentColor()
        {
            return Section.PalettePrefab.GetColor(Kind);
        }

        public void RefreshColor()
        {

            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = CurrentColor();

        }
		
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var stdColor = Handles.color;

            Handles.color = CurrentColor();

            Handles.DrawSolidDisc(transform.position, transform.forward, GlobalRadius);

            Handles.color = stdColor;

        }
#endif

        private bool CollidesWith(Transform subject)
        {
            Vector2 a = transform.position;
            Vector2 b = subject.position;

            var r = GlobalRadius;
            var rSqr = r * r;

            var dSqr = (a - b).SqrMagnitude();

            return dSqr < rSqr;

        }

        public void ResolveCollisions()
        {
            if (_kind == NoteKind.None)
                return;

            var c = 0;

            if (CollidesWith(PlayerManager.Player1.transform))
                c += 1;

            if (CollidesWith(PlayerManager.Player2.transform))
                c += 2;

            switch (c)
            {
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

        private void OnCollisionFirst()
        {
            switch (_kind)
            {
                case NoteKind.Any:
                case NoteKind.First:
                    PlayerManager.Player1.OnCollision();
                    OnCollision();
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

        private void OnCollisionSecond()
        {
            switch (_kind)
            {
                case NoteKind.Any:
                case NoteKind.Second:
                    PlayerManager.Player2.OnCollision();
                    OnCollision();
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

        private void OnCollisionBoth()
        {
            switch (_kind)
            {
                case NoteKind.Second:
                    PlayerManager.Player2.OnCollision();
                    OnCollision();
                    break;
                case NoteKind.First:
                    PlayerManager.Player2.OnCollision();
                    OnCollision();
                    break;
                case NoteKind.Any:
                case NoteKind.Both:
                    PlayerManager.Player1.OnCollision();
                    PlayerManager.Player2.OnCollision();
                    OnCollision();
                    break;
                default:
                    OnCollisionError();
                    break;
            }

        }

        private void OnCollision()
        {
            InstrumentManager.Instance.Play(Clip);

            if(Collision != null)
                Collision(this);

        }

        private static void OnCollisionError()
        {
            Debug.LogError("Unexpected result in collision detection.");
        }

    }
}
