using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor {
    public class SongWindow : EditorWindow {

        private Song target;

        [MenuItem("Window/Song")]
        public static void ShowWindow() {

            GetWindow(typeof(SongWindow));

        }

        void OnSelectionChange() {

            var g = Selection.activeGameObject;

            target = g != null ? g.GetComponentInParent<Song>() : null;

            Repaint();

        }

        void OnGUI() {

            title = "Song";

            if(target == null) {
                return;
            }

            EditorGUILayout.BeginHorizontal();

            OnRefresh();

            EditorGUILayout.EndHorizontal();

        }

        private void OnRefresh() {

            if(!GUILayout.Button("Refresh"))
                return;

            target.RefreshChildren();


        }

    }
}
