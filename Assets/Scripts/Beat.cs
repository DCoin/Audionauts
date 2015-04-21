using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts {
    public class Beat : MonoBehaviour {

        public float Radius;

        private IEnumerable<Note> Notes {

            get {
                return
                    from note in transform.GetComponentsInChildren<Note>(true)
                    where note.transform.parent == transform
                    select note;
            }

        }

        private Note AddNote(int index) {

            var section = GetComponentInParent<Section>();

            var note = (Note) Instantiate(section.NotePrefab, Vector3.zero, Quaternion.identity);

            note.transform.parent = transform;

            note.Index = index;

            note.transform.SetAsLastSibling();

            RefreshChildren();

            return note;

        }

        public NoteKind GetNoteKind(int i) {
            if(i < 0 || i >= noteCount)
                throw new IndexOutOfRangeException();

            foreach(var note in Notes.Where(note => i == note.Index)) {
                return note.Kind;
            }

            return NoteKind.None;
        }

        public Note this[int i] {
            get {
                if(i < 0 || i >= noteCount)
                    throw new IndexOutOfRangeException();

                foreach(var note in Notes.Where(note => i == note.Index)) {
                    return note;
                }

                return AddNote(i);

            }
        }

        private int noteCount;

        public void SetNoteCount(int count) {

            noteCount = count;

        }

        public void RefreshChildren() {

            var section = GetComponentInParent<Section>();

            float b = noteCount;

            foreach(var note in Notes) {

                if (note.Kind == NoteKind.None) {
                    DestroyImmediate(note.gameObject);
                    continue;
                }

                if (note.Index == -1) {
                    note.Index = note.transform.GetSiblingIndex();
                }

                var idx = note.Index;
                
                note.name = section.Notes[idx];

                float a = idx;

                note.transform.localPosition = Quaternion.Euler(0f, 0f, -a * 360f / b) * (new Vector3(0f, Radius, 0f));

            }

        }

        private void LateUpdate() {
            var traveller = StageManager.Instance.Traveller;

            var za = traveller.LastPosition.z;

            var z = transform.position.z;

            var zb = traveller.CurrentPosition.z;

            var diff = zb - za;

            if((zb < z) && (z < zb + diff * 1.5)) {
                foreach(var note in Notes) {
                    note.ResolveCollisions(true);
                }
            }

            if((za < z) && (z < zb)) {
                foreach(var note in Notes) {
                    note.ResolveCollisions(false);
                }
            }
        }

    }
}
