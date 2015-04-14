using Assets.Scripts;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Assets.Editor
{
    [CustomEditor(typeof(SoundBatch))]
    public class SoundBatchEditor : UnityEditor.Editor
    {

        private SoundBatch _soundBatch;

        public override void OnInspectorGUI()
        {

            _soundBatch = target as SoundBatch;

            if (target == null)
                return;

            DrawDefaultInspector();
            EditorGUILayout.Space();
            if (DropAreaGUI())
                return;
            EditorGUILayout.Space();
            
            ChildrenGUI();
            EditorGUILayout.Space();
            SortButton();
            ClearButton();
        }

        private void ChildrenGUI()
        {
            var children =
                from child
                    in _soundBatch.transform.GetComponentsInChildren<SoundBite>()
                where child.transform.parent == _soundBatch.transform 
                select child;

            var arr = children.ToArray();

            GUILayout.BeginVertical();

            for (int i = 0; i < arr.Length; ++i)
            {
                var child = arr[i];

                GUILayout.BeginHorizontal();

                if (GUILayout.Button(child.name, GUILayout.ExpandWidth(true)))
                {

                    Selection.activeObject = child.gameObject;
                }

                if(GUILayout.Button("X", GUILayout.Width(22f)))
                {
                    DestroyImmediate(child.gameObject);
                    return;

                }

                GUILayout.EndHorizontal();

            }

            GUILayout.EndVertical();
        }

        private void ClearButton()
        {
            if (!GUILayout.Button("Clear Batch")) 
                return;

            foreach (var child in _soundBatch.transform.GetComponentsInChildren<SoundBite>()
                .Where(child => child.transform.parent == _soundBatch.transform))
            {
                DestroyImmediate(child.gameObject);
            }
        }

        private void SortButton() {
            if(!GUILayout.Button("Sort Batch"))
                return;

            var children = 
                from child 
                    in _soundBatch.transform.GetComponentsInChildren<SoundBite>() 
                where child.transform.parent == _soundBatch.transform 
                orderby child.name ascending 
                select child;


            var arr = children.ToArray();

            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i].transform.SetSiblingIndex(i);
            }

        }

        public bool DropAreaGUI()
        {

            var added = false;

            var evt = Event.current;
            
            var dropArea = GUILayoutUtility.GetRect(0.0f, 128.0f, GUILayout.ExpandWidth(true));
            var defaultStyle = GUI.skin.box;
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
            GUI.Box(dropArea, "Drag new AudioClips here.");
            GUI.skin.box = defaultStyle;
            
            switch(evt.type) {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if(!dropArea.Contains(evt.mousePosition))
                        return false;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if(evt.type == EventType.DragPerform) {
                        DragAndDrop.AcceptDrag();

                        foreach(var clip in DragAndDrop.objectReferences.OfType<AudioClip>())
                        {
                            added = true;
                            var go = new GameObject(clip.name);
                            var soundBite = go.AddComponent<SoundBite>();
                            soundBite.Clip = clip;
                            go.transform.parent = _soundBatch.transform;
                        }
                    }
                    break;
            }

            return added;
        }
    }
}