using UnityEngine;

namespace Assets.Scripts.Managers {
    public class ModelManager : MonoBehaviour {
        public GameObject NoteModel;

        public static ModelManager Instance { get; private set; }

        private void Start() {
            if(Instance == null) {
                Instance = this;
            } else {
                Debug.LogError("A scene should only have one ModelManager");
            }
        }

    }
}