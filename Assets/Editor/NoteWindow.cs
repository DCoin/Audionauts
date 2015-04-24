using Assets.Scripts;
using Assets.Scripts.Enums;
using Assets.Scripts.Utilities;
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

            title = "Note";

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();

            OnNoteKinds();

            OnNoteMove();

            EditorGUILayout.EndVertical();

            OnNoteBite();

            EditorGUILayout.EndHorizontal();

        }

        private void OnNoteBite() {

            var section = _target.GetComponentInParent<Section>();

            if(section == null)
                return;

            EditorGUILayout.BeginHorizontal();

            foreach (var batch in section.SoundBatches)
            {
                EditorGUILayout.BeginVertical();

                if (GUILayout.Button(batch.name))
                {
                    Selection.activeObject = batch.gameObject;
                }

                EditorGUILayout.Space();

                var stdColor = GUI.color;

                foreach(var bite in batch.GetComponentsInImmediateChildren<SoundBite>())
                {

                    GUI.color = bite == _target.SoundBite ? Color.grey : stdColor;

                    var stdAlignment = GUI.skin.button.alignment;

                    GUI.skin.button.alignment = TextAnchor.MiddleLeft;

                    if (GUILayout.Button(bite.name))
                    {

                        AudioUtility.PlayClip(bite.Clip);

                        _target.SoundBite = bite;
                    }

                    GUI.skin.button.alignment = stdAlignment;

                }

                GUI.color = stdColor;

                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndHorizontal();

        }
        public void Update() {
            // This will only get called 10 times per second.
            Repaint();
            
        }

        private void OnNoteKinds()
        {

            var stdColor = GUI.color;

            foreach(var nk in (NoteKind[]) Enum.GetValues(typeof(NoteKind)))
            {

                GUI.color = nk == _target.Kind ? _target.CurrentColor : stdColor;

                var s = nk.ToString();

                if(GUILayout.Button(s, GUILayout.Height(48f), GUILayout.Width(128f)))
                {
                    _target.Kind = nk;

                    SceneView.RepaintAll();
                }

            }

            GUI.color = stdColor;

        }

        private void OnNoteMove() {

            if(GUILayout.Button("<", GUILayout.Height(48f), GUILayout.Width(128f))) {

                _target.Index--;
                _target.OnValidate();

            }

            if(GUILayout.Button(">", GUILayout.Height(48f), GUILayout.Width(128f))) {

                _target.Index++;
                _target.OnValidate();

            }


        }

    }
}
