using UnityEngine;

namespace Assets.Scripts.Managers {
    public class FadeManager : MonoBehaviour {

        public static FadeManager Instance { get; private set; }

        public string levelName;

        

        public float FadeSpeed = 0.8f;

        private float alpha = 1.0f;

        public float Progress { get { return 1f - alpha; } }

        private int drawDepth = -1000;

        private int fadeDirection = -1;

        public Texture2D FadeTexture;

        public KeyCode Key;

   

        private void Start() {
            if(Instance == null) {
                Instance = this;
            } else {
                Debug.LogError("A scene should only have one FadeManager");
            }
        }

        void Update() {

            if(Input.GetKeyDown(Key)) {
                EndScene();
            }


            alpha += fadeDirection * FadeSpeed * Time.deltaTime;

            if(alpha < 0f) {
                alpha = 0f;
                fadeDirection = 0;

            }

            if(alpha > 1f) {
                alpha = 1f;
                fadeDirection = 0;
                Application.LoadLevel(levelName);
            }


        }

        void OnGUI() {

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.depth = drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), FadeTexture);

        }

        public void EndScene() {

            fadeDirection = 1;

        }

    }
}