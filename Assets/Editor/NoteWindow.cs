using Assets.Scripts;
using Assets.Scripts.Enums;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor {
    public class NoteWindow : EditorWindow {

        private Note _target;

        [MenuItem("Window/Note")]
        public static void ShowWindow() {

            GetWindow(typeof(NoteWindow));

        }



        private void OnSelectionChange() {

            var g = Selection.activeGameObject;

            _target = g != null ? g.GetComponent<Note>() : null;

            Repaint();

        }

        private void OnGUI() {

            if(_target == null) {
                return;
            }

            EditorGUILayout.BeginVertical();

            OnNoteKinds();

            EditorGUILayout.EndVertical();

        }
        public void OnInspectorUpdate() {
            // This will only get called 10 times per second.
            Repaint();
        }

        private void OnNoteKinds() {

            var stdColor = GUI.color;

            foreach(var nk in (NoteKind[]) Enum.GetValues(typeof(NoteKind)))
            {

                GUI.color = nk == _target.Kind ? Color.grey : stdColor;

                var s = nk.ToString();

                if(GUILayout.Button(s, GUILayout.ExpandHeight(true)))
                {
                    _target.Kind = nk;

                    SceneView.RepaintAll();
                }

            }

            GUI.color = stdColor;

        }

    }
}
