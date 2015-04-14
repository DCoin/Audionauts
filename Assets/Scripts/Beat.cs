using System.Linq;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class Beat : MonoBehaviour {

        public float Radius;

        public Note[] Notes {
		
            get {

                var inotes = 
                    from note in transform.GetComponentsInChildren<Note>()
                    where note.transform.parent == transform
                    select note;

                return inotes.ToArray();

            }
		
        }

        public void SetNoteCount(int count) {

            var diff = Notes.Length - count;

            if(diff > 0) {

                RemoveNotes(diff);

            }

            if(diff < 0) {

                AddNotes(-diff);

            }

        }

	
        public void AddNotes(int count) {

            var section = GetComponentInParent<Section>();

            for(var i = 0; i < count; ++i) {

                var note = (Note) Instantiate(section.NotePrefab, Vector3.zero, Quaternion.identity);
			
                note.transform.parent = transform;
			
                note.transform.SetAsLastSibling();

            }

            RefreshChildren();

        }
	
        public void RemoveNotes(int count) {

            for(var i = 0; i < count; ++i) {

                DestroyImmediate(transform.GetChild(transform.childCount-1).gameObject);

            }
		
            RefreshChildren();
		
        }

        private void RefreshChildren() {

            var section = GetComponentInParent<Section>();

            float b = Notes.Length;
		
            foreach(var note in Notes) {

                note.RefreshColor();

                var idx = note.transform.GetSiblingIndex();

                note.name = section.Notes[idx];

                float a = idx;
			
                note.transform.localPosition = Quaternion.Euler(0f, 0f, -a * 360f / b) * (new Vector3(0f, Radius, 0f));

            }

        }

        private void LateUpdate()
        {
            var traveller = PlayerManager.Instance.Traveller;

            var za = traveller.LastPosition.z;

            var z = transform.position.z;

            var zb = traveller.CurrentPosition.z;

            if (!(za < z) || !(z < zb)) 
                return;

            foreach (var note in Notes)
            {
                note.ResolveCollisions();
            }
        }

    }
}
