using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class SectionWindow : EditorWindow {

        private Section target;

        [MenuItem ("Window/Section")]
        public static void ShowWindow() {
		
            GetWindow (typeof(SectionWindow));
		
        }

        void OnSelectionChange() {

            var g = Selection.activeGameObject;

            target = g != null ? g.GetComponentInParent<Section>() : null;

            Repaint();

        }

        void OnGUI() {

            title = "Section";

            if(target == null) {
                return;
            }

            EditorGUILayout.BeginHorizontal();

            OnRefresh();

            EditorGUILayout.EndHorizontal();

        }

        private void OnRefresh() {

            if (!GUILayout.Button("Refresh"))
                return;

            target.RefreshChildren();


        }

        private void OnAddAt(int index) {

            EditorGUILayout.BeginVertical();

            if(index == 0 || index == target.Bars.Length) {

                GUILayout.Label("", GUILayout.Height(24f));
			
            } else {

                OnBarSwap(index);

            }

            if(GUILayout.Button("+", GUILayout.Width(24f), GUILayout.ExpandHeight(true))) {

                target.AddBar(index);

                SceneView.RepaintAll();

            }

            GUILayout.Label("", GUILayout.Height(24f));

            EditorGUILayout.EndVertical();
		
        }

        private void OnBarFocus(int index) {

            var bar = target.Bars[index];

            if (!GUILayout.Button(bar.name, GUILayout.Height(24f))) 
                return;

            Selection.activeObject = bar.gameObject;
			
            SceneView.lastActiveSceneView.FrameSelected();
        }
	
        private bool OnBarRemove(int index) {

            if (!GUILayout.Button("-", GUILayout.Height(24f))) 
                return false;

            target.RemoveBar(index);
			
            Selection.activeObject = target.gameObject;

            SceneView.RepaintAll();

            return true;

        }

        private void OnBarSwap(int index) {
            if ((index >= target.Bars.Length) || !GUILayout.Button("<>", GUILayout.Width(24f), GUILayout.Height(24f)))
                return;

            target.MoveBar(index-1, index);

            SceneView.RepaintAll();
        }

        public void Update() {
            // This will only get called 10 times per second
            Repaint();
        }
	
    }
}
