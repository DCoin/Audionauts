using System.Linq;
using UnityEngine;

namespace Assets.Scripts {
    public class Song : MonoBehaviour {


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
