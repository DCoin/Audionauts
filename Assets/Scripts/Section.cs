﻿using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Section : MonoBehaviour {

        public ColorPalette PalettePrefab;
        public AudioClip[] Clips;
        public Bar BarPrefab;
        public Beat BeatPrefab;
        public Note NotePrefab;

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

            bar.AddBeat();

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

                b.name = "Bar" + idx;

                b.transform.localPosition = new Vector3(0f, 0f, idx);

                var s = b.transform.localScale;
                s.z = 1f;
                b.transform.localScale = s;

            }

        }

    }
}