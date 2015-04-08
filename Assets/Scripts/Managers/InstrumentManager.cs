using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class InstrumentManager : MonoBehaviour
    {

        public static InstrumentManager Instance { get; private set; }

        private void Start()
        {

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("A scene should only have one InstrumentManager");
            }
        }

        private void Update()
        {
            foreach (var source in GetComponentsInChildren<AudioSource>().Where(source => !source.isPlaying))
            {
                Destroy(source.gameObject);
            }
        }

        public void Play(AudioClip clip)
        {
            if (clip == null)
            {
                Debug.LogError("Asked to play null clip.");
                return;
            }

            var obj = new GameObject();
            
            var src = obj.AddComponent<AudioSource>();
            src.clip = clip;

            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
            obj.transform.parent = transform;

            src.Play();

        }

    }
}

