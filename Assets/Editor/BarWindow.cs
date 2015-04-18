using Assets.Scripts;
using Assets.Scripts.Enums;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class BarWindow : EditorWindow {

        private Bar _target;

        private readonly GUIStyle _style = new GUIStyle { alignment = TextAnchor.MiddleCenter };

        private static int _start;

        private int _bump;

        [MenuItem ("Window/Bar")]
        public static void ShowWindow() {
		
            GetWindow (typeof(BarWindow));
		
        }

        private void OnSelectionChange() {

            var g = Selection.activeGameObject;

            _target = g != null ? g.GetComponentInParent<Bar>() : null;

            Repaint();

        }

        private void OnGUI() {

            this.title = "Bar";

            if(_target == null) {
                return;
            }

            _start += _bump;
            _bump = 0;


            EditorGUILayout.BeginHorizontal();

            OnNotesColumn();	

            OnBarsAndBeats ();

            EditorGUILayout.EndHorizontal();

        }

        private string[] Notes { get { return Section.Notes; } }

        private Section Section { get { return _target.GetComponentInParent<Section>(); } }

        private void OnNotesColumn() {

            EditorGUILayout.BeginVertical();


            var stdColor = GUI.color;

            GUI.color = Color.gray;

            if(GUILayout.Button("", GUILayout.Height (24f))) {

                _bump = (_bump + 1) % Notes.Length;

            }

            GUI.color = stdColor;

            //EditorGUILayout.LabelField("Notes", style, GUILayout.Height(32f), GUILayout.Width(64f));

            for(var n = 0; n < Notes.Length; ++n) {

                var idx = (n + _start) % Notes.Length;

                var noteName = Notes[idx];

                if(GUILayout.Button(noteName, GUILayout.ExpandHeight(true))) {

                    _bump = n;

                }
			
            }

            GUI.color = Color.gray;
		
            if(GUILayout.Button("", GUILayout.Height (24f))) {
			
                _bump--;

                if(_bump < 0) {
                    _bump += Notes.Length;
                }

            }
		
            GUI.color = stdColor;
		
            EditorGUILayout.EndVertical ();

        }

        private void OnBarsAndBeats() {

            EditorGUILayout.BeginHorizontal(_style);

            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();

            OnBarFocus();

            EditorGUILayout.EndHorizontal();

            for(var n = 0; n < Notes.Length; ++n) {

                var noteIdx = (n + _start) % Notes.Length;

                EditorGUILayout.BeginHorizontal();

                int i = 0;

                foreach (var beat in _target.Beats)
                {

                        

                    if (i == 4) {
                        i -= 4;
                        EditorGUILayout.Space();
                    }

                    i++;

                    beat.SetNoteCount(Notes.Length);

                    OnNoteToggle(beat.Notes[noteIdx]);
                }

                EditorGUILayout.EndHorizontal();
				
            }

            EditorGUILayout.BeginHorizontal();

            OnBeatAdd();

            OnBeatRemove();

            GUILayout.Label(_target.Beats.Length.ToString() + " Beats");

            EditorGUILayout.EndHorizontal();
			
            EditorGUILayout.EndVertical();
		
            EditorGUILayout.EndHorizontal();

        }

        private void OnBarFocus() {

            if (!GUILayout.Button(_target.name, GUILayout.Height(24f))) 
                return;

            Selection.activeObject = _target.gameObject;
			
            SceneView.lastActiveSceneView.FrameSelected();
        }

        private void OnBeatAdd() {

            if (!GUILayout.Button("*2", GUILayout.Height(24f))) return;

            var l = _target.Beats.Length;

                
            for (var i = 0; i < l; ++i) {
                _target.AddBeat();
            }
            
            SceneView.RepaintAll();
        }
	
        private void OnBeatRemove() {

            if (!GUILayout.Button("/2", GUILayout.Height(24f))) return;

            var l = _target.Beats.Length / 2;


            for(var i = 0; i < l; ++i) {
                _target.RemoveBeat();
            }

            SceneView.RepaintAll();
        }

        public void Update() {
            // This will only get called 10 times per second.
            Repaint();
        }

        private void OnNoteToggle(Note note) {

            var stdColor = GUI.color;

            if (note.Kind != NoteKind.None) {

                GUI.color = Section.PalettePrefab.GetColor(note.Kind);

            } else {
                GUI.color = Color.black;
            }

            if(GUILayout.Button("", GUILayout.ExpandHeight (true))) {

                Selection.activeObject = note.gameObject;

                SceneView.lastActiveSceneView.FrameSelected();
			
                //note.Kind = note.Kind.Next();

                //SceneView.RepaintAll();
			
            }
		
            GUI.color = stdColor;
		
        }
	
    }
}
