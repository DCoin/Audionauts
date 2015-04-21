using System.Linq;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts {
    public class Song : MonoBehaviour {

        public int MaxDistance = 4;
        public int MinDistance = -1;

        public Section[] Sections {

            get {
                var isections =
                    from section in transform.GetComponentsInChildren<Section>()
                    where section.transform.parent == transform
                    orderby section.transform.GetSiblingIndex() ascending 
                    select section;

                return isections.ToArray();
            }

        }

        public void Update() {

            var sm = StageManager.Instance;

            var sz = sm.Stage.localScale.z;
            var za = sm.Traveller.CurrentPosition.z;

            var max = MaxDistance*sz;
            var min = MinDistance*sz;

            foreach (var beat in transform.GetComponentsInChildren<Beat>(true)) {

                var zb = beat.transform.position.z;

                var dz = zb - za;

                var go = beat.gameObject;

                go.SetActive(dz < max);
                
                if (dz < min) {
                    Destroy(go);
                } 

            }

        }

        public void RefreshChildren() {

            var ss = Sections;
            var sl = ss.Length;

            float z = 0f;

            for (int i = 0; i < sl; ++i) {

                Section s = ss[i];

                var pos = s.transform.localPosition;

                pos.z = z;

                s.transform.localPosition = pos;

                z += s.Bars.Length;

                s.RefreshChildren();


            }

        }
    }
}
