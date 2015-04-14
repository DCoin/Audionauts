using Assets.Scripts;
using Assets.Scripts.Enums;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class SectionWindow : EditorWindow {

        private Section _target;

        private static int _start;

        private const int BarsShown = 3;

        private static int _currentBar;

        private readonly GUIStyle _style = new GUIStyle { alignment = TextAnchor.MiddleCenter };

        private int _bump;
	
        public int CurrentBar {
            get {

                return CurrentBar = _currentBar;

            }
            set {

                if(value < 0) {
                    _currentBar = 0;
                } else if(value >= (_target.Bars.Length - BarsShown)) {
                    var b = _target.Bars.Length - BarsShown;
                    _currentBar = b < 0 ? 0 : b;
                } else {
                    _currentBar = value;
                }

            }
        }

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

            _start += _bump;
            _bump = 0;

            EditorGUILayout.BeginHorizontal();

            OnNotesColumn();	

            OnLeftButton();

            OnBarsAndBeats ();

            OnRightButton();

            EditorGUILayout.EndHorizontal();

        }

        private void OnNotesColumn() {

            EditorGUILayout.BeginVertical();


            var stdColor = GUI.color;

            GUI.color = Color.gray;

            if(GUILayout.Button("", GUILayout.Height (24f))) {

                _bump = (_bump + 1) % _target.Notes.Length;

            }

            GUI.color = stdColor;

            //EditorGUILayout.LabelField("Notes", style, GUILayout.Height(32f), GUILayout.Width(64f));

            for(var n = 0; n < _target.Notes.Length; ++n) {

                var idx = (n + _start) % _target.Notes.Length;

                var noteName = _target.Notes[idx];

                if(GUILayout.Button(noteName, GUILayout.ExpandHeight(true))) {

                    //bump = n;

                    

                }
			
            }

            GUI.color = Color.gray;
		
            if(GUILayout.Button("", GUILayout.Height (24f))) {
			
                _bump--;

                if(_bump < 0) {
                    _bump += _target.Notes.Length;
                }

            }
		
            GUI.color = stdColor;
		
            EditorGUILayout.EndVertical ();

        }

        private void OnLeftButton() {

            var enabled = CurrentBar > 0;

            var stdColor = GUI.color;

            if (!enabled)
                GUI.color = Color.grey;


            if (GUILayout.Button ("<", GUILayout.Width(20f), GUILayout.ExpandHeight (true))
                && enabled) {
                CurrentBar--;
            }

            GUI.color = stdColor;

        }

        private void OnRightButton()
        {

            var enabled = CurrentBar + BarsShown < _target.Bars.Length;

            var stdColor = GUI.color;

            if (!enabled)
                GUI.color = Color.grey;

            if (GUILayout.Button (">", GUILayout.Width(20f), GUILayout.ExpandHeight (true)) 
                && enabled) {
                CurrentBar++;
            }

            GUI.color = stdColor;

        }

        private void OnBarsAndBeats() {

            EditorGUILayout.BeginHorizontal(_style);

            OnAddAt(0);

            for (int barIdx = CurrentBar; barIdx < (CurrentBar+BarsShown) && barIdx < _target.Bars.Length; ++barIdx) {

                EditorGUILayout.BeginVertical();

                EditorGUILayout.BeginHorizontal();

                OnBarFocus(barIdx);

                if(OnBarRemove(barIdx)) {

                    return;

                }

                EditorGUILayout.EndHorizontal();

                for(var n = 0; n < _target.Notes.Length; ++n) {

                    var noteIdx = (n + _start) % _target.Notes.Length;

                    EditorGUILayout.BeginHorizontal();

                    var bar = _target.Bars[barIdx];

                    foreach (var beat in bar.Beats)
                    {
                        beat.SetNoteCount(_target.Notes.Length);

                        OnNoteToggle(beat.Notes[noteIdx]);
                    }

                    EditorGUILayout.EndHorizontal();
				
                }

                EditorGUILayout.BeginHorizontal();

                OnBeatAdd(barIdx);
			
                OnBeatRemove(barIdx);

                EditorGUILayout.EndHorizontal();
			
                EditorGUILayout.EndVertical();

                OnAddAt(barIdx+1);

            }
		
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

        private void OnBeatAdd(int index) {
            if (!GUILayout.Button("+1", GUILayout.Height(24f))) 
                return;

            _target.Bars[index].AddBeat();

            SceneView.RepaintAll();
        }
	
        private void OnBeatRemove(int index) {


            if(GUILayout.Button("-1", GUILayout.Height(24f))) {

                _target.Bars[index].RemoveBeat();

            }

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
        public void OnInspectorUpdate() {
            // This will only get called 10 times per second.
            Repaint();
        }

        private void OnNoteToggle(Note note) {

            var stdColor = GUI.color;

            if(note.Kind != NoteKind.None) {

                GUI.color = _target.PalettePrefab.GetColor(note.Kind);

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
