using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Slice))]
public class SliceEditor : Editor {

	private void OnSceneGUI () {
		Slice slice = target as Slice;

		Handles.color = Color.Lerp (Color.clear, slice.GetColor (), 0.4f);

		Vector3 c = slice.transform.parent.position;
		Vector3 p = slice.transform.position;
		Vector3 pc = p - c;
		float r = pc.magnitude;
		float a = slice.angle / 2f;
		Handles.DrawSolidArc (c, Vector3.forward, pc, a, r);
		Handles.DrawSolidArc (c, Vector3.back   , pc, a, r);
	}

}
