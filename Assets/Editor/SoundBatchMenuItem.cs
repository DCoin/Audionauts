using UnityEditor;
using UnityEngine;

namespace Assets.Scripts {

    public class SoundBatchMenuItem {

        [MenuItem("GameObject/Create Other/SoundBatch")]
        static void CreateSoundBatch() {

            var go = new GameObject("SoundBatch");
            go.AddComponent<SoundBatch>();

        }

    }
}
