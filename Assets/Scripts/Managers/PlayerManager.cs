using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public Traveller Traveller;
        public CollisionHandler Player1;
        public CollisionHandler Player2;

        public static PlayerManager Instance { get; private set; }

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