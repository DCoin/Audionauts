using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Linq;

[CustomEditor(typeof(Stage))]
public class StageEditor : Editor {

	public override void OnInspectorGUI() {
		
		base.OnInspectorGUI();
		
		if (GUILayout.Button ("Adjust Bars"))
			AdjustBars(target as Stage);

		if (GUILayout.Button ("Adjust Beats"))
			AdjustBeats(target as Stage);

	    if (GUILayout.Button("Clean Notes"))
	    {
	        CleanNotes(target as Stage);
	    }

	}

	private static int GetNumberSuffix(string prefix, string s) {

		string suffix = s.Remove (0, prefix.Length);

		return int.Parse (suffix);

	}

	void AdjustBars(Stage stage) {

		foreach (Transform bar in stage.GetBars()) {
			try {
				int i = GetNumberSuffix(stage.barPrefix, bar.gameObject.name);
				
				Vector3 pos = bar.localPosition;
				pos.z = (float) (i - 1) * 4;
				bar.localPosition = pos;
			} catch (Exception) {}
		}

	}

	void AdjustBeats(Stage stage) {

		foreach (Transform bar in stage.GetBars()) {

			Transform[] children = bar.GetComponentsInChildren<Transform>();
			
			foreach(Transform child in children) {

				if(child.parent != bar.transform)
					continue;
				
				string name = child.gameObject.name;
				
				if(!name.StartsWith(stage.beatPrefix))
					continue;
				try {
					int i = GetNumberSuffix(stage.beatPrefix, name);
					Vector3 pos = child.localPosition;
					pos.z = ((float) (i - 1)) * stage.beatsMultiplier;
					child.localPosition = pos;
				} catch (Exception) {}

			}
			
		}

	}

    static void CleanNotes(Stage stage)
    {
        if (stage == null) return;


        foreach (var s in stage.transform.GetComponentsInChildren<Slice>(true))
        {
            var go = s.gameObject;

            if (go.activeSelf)
            {

                continue;
            }
            else
            {
                DestroyImmediate(go);

                Debug.Log("FOOOOO");

            }

        }

    }
}
