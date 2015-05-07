using Assets.Scripts.CollisionHandlers;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class StageManager : MonoBehaviour
    {
        public Traveller Traveller;
        public CollisionHandler Player1;
        public CollisionHandler Player2;
		public TailEmitter Tail1;
		public TailEmitter Tail2;
        public Transform Stage;

        public static StageManager Instance { get; private set; }

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("A scene should only have one PlayerManager");
            }
        }

    }
}