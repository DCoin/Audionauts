using Assets.Scripts;
using Assets.Scripts.Enums;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class SectionWindow : EditorWindow {

        private Section _target;

        [MenuItem ("Window/Section")]
        public static void ShowWindow() {
		
            GetWindow (typeof(SectionWindow));
		
        }

        private void OnSelectionChange() {

            var g = Selection.activeGameObject;

            _target = g != null ? g.GetComponentInParent<Section>() : null;

            Repaint();

        }

        private void OnGUI() {

            if(_target == null) {
                return;
            }

            title = "Section";

            EditorGUILayout.BeginHorizontal();



            EditorGUILayout.EndHorizontal();

        }

        private void OnAddAt(int index) {

            EditorGUILayout.BeginVertical();

            if(index == 0 || index == _target.Bars.Length) {

                GUILayout.Label("", GUILayout.Height(24f));
			
            } else {

                OnBarSwap(index);

            }

            if(GUILayout.Button("+", GUILayout.Width(24f), GUILayout.ExpandHeight(true))) {

                _target.AddBar(index);

                SceneView.RepaintAll();

            }

            GUILayout.Label("", GUILayout.Height(24f));

            EditorGUILayout.EndVertical();
		
        }

        private void OnBarFocus(int index) {

            var bar = _target.Bars[index];

            if (!GUILayout.Button(bar.name, GUILayout.Height(24f))) 
                return;

            Selection.activeObject = bar.gameObject;
			
            SceneView.lastActiveSceneView.FrameSelected();
        }
	
        private bool OnBarRemove(int index) {

            if (!GUILayout.Button("-", GUILayout.Height(24f))) 
                return false;

            _target.RemoveBar(index);
			
            Selection.activeObject = _target.gameObject;

            SceneView.RepaintAll();

            return true;

        }

        private void OnBarSwap(int index) {
            if ((index >= _target.Bars.Length) || !GUILayout.Button("<>", GUILayout.Width(24f), GUILayout.Height(24f)))
                return;

            _target.MoveBar(index-1, index);

            SceneView.RepaintAll();
        }

        public void Update() {
            // This will only get called 10 times per second
            Repaint();
        }
	
    }
}
