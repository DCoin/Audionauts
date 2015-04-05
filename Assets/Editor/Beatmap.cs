using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using Audionauts.Enums;

public class Beatmap : EditorWindow {

	private Section target = null;

	private static int start = 0;
	
	private static int barsShown = 3;

	private static int _currentBar = 0;

	private GUIStyle style = new GUIStyle () { alignment = TextAnchor.MiddleCenter };

	private int bump = 0;
	
	public int CurrentBar {
		get {

			return CurrentBar = _currentBar;

		}
		set {

			if(value < 0) {
				_currentBar = 0;
			} else if(value >= (target.Bars.Length - barsShown)) {
				int b = target.Bars.Length - barsShown;
				_currentBar = b < 0 ? 0 : b;
			} else {
				_currentBar = value;
			}

		}
	}

	[MenuItem ("Window/Beatmap")]
	public static void ShowWindow() {
		
		EditorWindow.GetWindow (typeof(Beatmap));
		
	}

	void OnSelectionChange() {

		GameObject g = Selection.activeGameObject;

		if(g != null) {

			target = g.GetComponentInParent<Section>();

		} else {

			target = null;

		}

		this.Repaint();

	}

	void OnGUI() {

		if(target == null) {
			return;
		}

		start += bump;
		bump = 0;

		EditorGUILayout.BeginHorizontal();

		OnNotesColumn();	

		OnLeftButton();

		OnBarsAndBeats ();

		OnRightButton();

		EditorGUILayout.EndHorizontal();

	}

	private void OnNotesColumn() {

		EditorGUILayout.BeginVertical();


		Color stdColor = GUI.color;

		GUI.color = Color.gray;

		if(GUILayout.Button("", GUILayout.Height (24f))) {

			bump = (bump + 1) % target.sources.Length;

		}

		GUI.color = stdColor;

		//EditorGUILayout.LabelField("Notes", style, GUILayout.Height(32f), GUILayout.Width(64f));

		for(int n = 0; n < target.sources.Length; ++n) {

			int idx = (n + start) % target.sources.Length;

			AudioSource aus = target.sources[idx];

			if(GUILayout.Button(aus.name, GUILayout.ExpandHeight (true))) {

				//bump = n;

				AudioUtil.PlayClip(aus.clip);

			}
			
		}

		GUI.color = Color.gray;
		
		if(GUILayout.Button("", GUILayout.Height (24f))) {
			
			bump--;

			if(bump < 0) {
				bump += target.sources.Length;
			}

		}
		
		GUI.color = stdColor;
		
		EditorGUILayout.EndVertical ();

	}

	private void OnLeftButton() {

		if (GUILayout.Button ("<", GUILayout.Width(20f), GUILayout.ExpandHeight (true))) {
			CurrentBar--;
		}

	}

	private void OnRightButton() {

		if (GUILayout.Button (">", GUILayout.Width(20f), GUILayout.ExpandHeight (true))) {
			CurrentBar++;
		}

	}

	private void OnBarsAndBeats() {

		EditorGUILayout.BeginHorizontal(style);

		OnAddAt(0);

		for (int barIdx = CurrentBar; barIdx < (CurrentBar+barsShown) && barIdx < target.Bars.Length; ++barIdx) {

			EditorGUILayout.BeginVertical();

			EditorGUILayout.BeginHorizontal();

			OnBarFocus(barIdx);

			if(OnBarRemove(barIdx)) {

				return;

			}

			EditorGUILayout.EndHorizontal();

			for(int n = 0; n < target.sources.Length; ++n) {
				
				int noteIdx = (n + start) % target.sources.Length;

				EditorGUILayout.BeginHorizontal();

				Bar bar = target.Bars[barIdx];

				for(int beatIdx = 0; beatIdx < bar.Beats.Length; ++beatIdx) {

					Beat beat = bar.Beats[beatIdx];

					beat.SetNoteCount(target.sources.Length);

					OnNoteToggle(bar.Beats[beatIdx].Notes[noteIdx]);

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

		if(index == 0 || index == target.Bars.Length) {

			GUILayout.Label("", GUILayout.Height(24f));
			
		} else {

			OnBarSwap(index);

		}

		if(GUILayout.Button("+", GUILayout.Width(24f), GUILayout.ExpandHeight(true))) {

			target.AddBar(index);

		}

		GUILayout.Label("", GUILayout.Height(24f));

		EditorGUILayout.EndVertical();
		
	}

	private void OnBarFocus(int index) {

		Bar bar = target.Bars[index];

		if(GUILayout.Button(bar.name, GUILayout.Height(24f))) {

			Selection.activeObject = bar.gameObject;
			
			SceneView.lastActiveSceneView.FrameSelected();
			
		}

	}

	private void OnBeatAdd(int index) {

		if(GUILayout.Button("+1", GUILayout.Height(24f))) {

			target.Bars[index].AddBeat();
		}

	}
	
	private void OnBeatRemove(int index) {


		if(GUILayout.Button("-1", GUILayout.Height(24f))) {

			target.Bars[index].RemoveBeat();

		}

	}
	
	private bool OnBarRemove(int index) {

		if(GUILayout.Button("-", GUILayout.Height(24f))) {
			
			target.RemoveBar(index);
			
			Selection.activeObject = target.gameObject;

			return true;
		}

		return false;

	}

	private void OnBarSwap(int index) {

		if ((index < target.Bars.Length) && GUILayout.Button ("<>", GUILayout.Width(24f), GUILayout.Height(24f))) {
			
			target.MoveBar(index-1, index);
			
		}

	}

	private void OnNoteToggle(Note note) {

		Color stdColor = GUI.color;


		if(note.Kind != NoteKind.None) {

			GUI.color = target.palettePrefab.GetColor(note.Kind);

		}

		if(GUILayout.Button("", GUILayout.ExpandHeight (true))) {
			
			note.Kind = note.Kind.Next();
			
		}
		
		GUI.color = stdColor;
		
	}
	
}
