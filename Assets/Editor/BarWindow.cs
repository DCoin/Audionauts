using Assets.Scripts;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor {
    public class BarWindow : EditorWindow {

        private Bar target;

        private readonly GUIStyle style = new GUIStyle { alignment = TextAnchor.MiddleCenter };

        private static int start;

        private int bump;

        [MenuItem("Window/Bar")]
        public static void ShowWindow() {

            GetWindow(typeof(BarWindow));

        }

        private void OnSelectionChange() {

            var g = Selection.activeGameObject;

            target = g != null ? g.GetComponentInParent<Bar>() : null;

            Repaint();

        }

        private void OnGUI() {

            title = "Bar";

            if(target == null) {
                return;
            }

            start += bump;
            bump = 0;


            EditorGUILayout.BeginHorizontal();

            OnNotesColumn();

            OnBarsAndBeats();

            EditorGUILayout.EndHorizontal();

        }

        private string[] Notes { get { return Section.Notes; } }

        private Section Section { get { return target.GetComponentInParent<Section>(); } }

        private void OnNotesColumn() {

            EditorGUILayout.BeginVertical();


            var stdColor = GUI.color;

            GUI.color = Color.gray;

            if(GUILayout.Button("", GUILayout.Height(24f))) {

                bump = (bump + 1) % Notes.Length;

            }

            GUI.color = stdColor;

            //EditorGUILayout.LabelField("Notes", style, GUILayout.Height(32f), GUILayout.Width(64f));

            for(var n = 0; n < Notes.Length; ++n) {

                var idx = (n + start) % Notes.Length;

                var noteName = Notes[idx];

                if(GUILayout.Button(noteName, GUILayout.ExpandHeight(true))) {

                    bump = n;

                }

            }

            GUI.color = Color.gray;

            if(GUILayout.Button("", GUILayout.Height(24f))) {

                bump--;

                if(bump < 0) {
                    bump += Notes.Length;
                }

            }

            GUI.color = stdColor;

            EditorGUILayout.EndVertical();

        }

        private void OnBarsAndBeats() {

            EditorGUILayout.BeginHorizontal(style);

            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();

            OnBarFocus();

            EditorGUILayout.EndHorizontal();

            for(var n = 0; n < Notes.Length; ++n) {

                var noteIdx = (n + start) % Notes.Length;

                EditorGUILayout.BeginHorizontal();

                int i = 0;

                foreach(var beat in target.Beats) {



                    if(i == 4) {
                        i -= 4;
                        EditorGUILayout.Space();
                    }

                    i++;

                    beat.SetNoteCount(Notes.Length);

                    OnNoteToggle(beat, noteIdx);
                }

                EditorGUILayout.EndHorizontal();

            }

            EditorGUILayout.BeginHorizontal();

            OnBeatAdd();

            OnBeatRemove();

            GUILayout.Label(target.Beats.Length.ToString() + " Beats");

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

        }

        private void Preview() {
            
        }

        private void OnBarFocus() {

            if(!GUILayout.Button(target.name, GUILayout.Height(24f)))
                return;

            Selection.activeObject = target.gameObject;

            Preview();

            SceneView.lastActiveSceneView.FrameSelected();
        }

        private void OnBeatAdd() {

            if(!GUILayout.Button("*2", GUILayout.Height(24f)))
                return;

            var l = target.Beats.Length;


            for(var i = 0; i < l; ++i) {
                target.AddBeat();
            }

            SceneView.RepaintAll();
        }

        private void OnBeatRemove() {

            if(!GUILayout.Button("/2", GUILayout.Height(24f)))
                return;

            var l = target.Beats.Length / 2;


            for(var i = 0; i < l; ++i) {
                target.RemoveBeat();
            }

            SceneView.RepaintAll();
        }

        public void Update() {
            // This will only get called 10 times per second.
            Repaint();
        }

        private void OnNoteToggle(Beat beat, int noteIdx) {

            var stdColor = GUI.color;

            var kind = beat.GetNoteKind(noteIdx);

            if(kind != NoteKind.None) {

                GUI.color = Section.PalettePrefab.GetColor(kind);

            } else {
                GUI.color = Color.black;
            }

            if(GUILayout.Button("", GUILayout.ExpandHeight(true))) {

                var note = beat[noteIdx];

                Selection.activeObject = note.gameObject;

                var v = SceneView.lastActiveSceneView;
                if(v != null)
                    v.FrameSelected();

                Section.RefreshChildren();

                //note.Kind = note.Kind.Next();

                //SceneView.RepaintAll();

            }

            GUI.color = stdColor;

        }

    }
}
