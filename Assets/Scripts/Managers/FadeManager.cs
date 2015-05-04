using UnityEngine;

namespace Assets.Scripts.Managers {
    public class FadeManager : MonoBehaviour {

        public delegate void OnFadeOut();

        public static FadeManager Instance { get; private set; }

        private OnFadeOut action;

        public float FadeSpeed = 0.8f;

        private float alpha = 1.0f;

        public float Progress { get { return 1f - alpha; } }

        private int drawDepth = -1000;

        private int fadeDirection = -1;

        public Texture2D FadeTexture;
   

        private void Start() {
            if(Instance == null) {
                Instance = this;
            } else {
                Debug.LogError("A scene should only have one FadeManager");
            }
        }

        void Update() {

            alpha += fadeDirection * FadeSpeed * Time.deltaTime;

            if(alpha < 0f) {
                alpha = 0f;
                fadeDirection = 0;

            }

            if(alpha > 1f) {
                alpha = 1f;
                fadeDirection = 0;
                if (action != null) {
                    action();
                    action = null;
                }
            }


        }

        void OnGUI() {

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.depth = drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), FadeTexture);

        }

        public void EndScene(OnFadeOut act) {

            if (action != null) 
                return;

            action = act;

            fadeDirection = 1;

        }

    }
}