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

        public void Play(AudioSource source)
        {
            if (source == null)
            {
                Debug.LogError("Asked to play null source.");
                return;
            }


            var obj = (AudioSource) Instantiate(source, Vector3.zero, Quaternion.identity);

            obj.transform.parent = transform;

        }

    }
}

