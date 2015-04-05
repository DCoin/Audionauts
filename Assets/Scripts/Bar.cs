using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Bar : MonoBehaviour {

        public Beat[] Beats {
		
            get {
                var ibeats = 
                    from note in transform.GetComponentsInChildren<Beat>()
                    where note.transform.parent == transform
                    select note;
			
                return ibeats.ToArray();
            }
		
        }

        public void AddBeat() {

            var section = GetComponentInParent<Section>();

            var beat = (Beat) Instantiate(section.BeatPrefab, Vector3.zero, Quaternion.identity);
		
            beat.transform.parent = transform;

            beat.transform.SetAsLastSibling();

            RefreshChildren();
		
        }

        public void RemoveBeat() {

            if(transform.childCount <= 1) {

                return;

            }

            var t = transform.GetChild(transform.childCount-1);
		
            DestroyImmediate(t.gameObject);
		
            RefreshChildren();
		
        }

        private void RefreshChildren() {

            float b = Beats.Length;

            foreach(var beat in Beats) {
			
                var idx = beat.transform.GetSiblingIndex();
			
                beat.name = "Beat" + idx;

                float a = idx;

                beat.transform.localPosition = new Vector3(0f, 0f, a/b);
			
            }
		
        }
    }
}
