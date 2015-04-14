using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    


    public class SoundBatch : MonoBehaviour
    {
        [MenuItem("GameObject/Create Other/SoundBatch")]
        static void CreateSoundBatch() {

            var go = new GameObject("SoundBatch");
            go.AddComponent<SoundBatch>();
            
        }


    }
}
