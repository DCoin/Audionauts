using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Section : MonoBehaviour {

        public ColorPalette PalettePrefab;
        public Bar BarPrefab;
        public string[] Notes;
        public SoundBatch[] SoundBatches;
        public Beat BeatPrefab;
        public Note NotePrefab;

        public Section NextSection;

        void OnValidate() {

            if (NextSection == null)
                return;

            var pos = NextSection.transform.localPosition;
            pos.z = Bars.Length + transform.localPosition.z;
            NextSection.transform.localPosition = pos;

        }

        public Bar[] Bars {
		
            get {
                var ibars = 
                    from note in transform.GetComponentsInChildren<Bar>()
                    where note.transform.parent == transform
                    select note;
			
                return ibars.ToArray();
            }
		
        }

        public void AddBar(int index) {

            var bar = (Bar) Instantiate(BarPrefab, Vector3.zero, Quaternion.identity);

            bar.transform.parent = transform;

            bar.transform.SetSiblingIndex(index);

            for (int i = 0; i < 4; ++i)
            {
                bar.AddBeat();
            }

            RefreshChildren();

        }

        public void RemoveBar(int index) {

            var t = transform.GetChild(index);

            DestroyImmediate(t.gameObject);

            RefreshChildren();

        }

        public void MoveBar(int oldIndex, int newIndex) {

            var t = transform.GetChild(oldIndex);

            t.SetSiblingIndex(newIndex);

            RefreshChildren();

        }

        private void RefreshChildren() {

            foreach(var b in Bars) {

                var idx = b.transform.GetSiblingIndex();

                b.name = "Bar" + (idx+1);

                b.transform.localPosition = new Vector3(0f, 0f, idx);

                var s = b.transform.localScale;
                s.z = 1f;
                b.transform.localScale = s;

            }

        }

    }
}
