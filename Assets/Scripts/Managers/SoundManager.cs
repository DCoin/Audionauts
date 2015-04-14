using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Managers
{
    public class SoundManager : MonoBehaviour
    {

        public AudioSource AudioSourcePrefab;

        private List<AudioSource> _audioSourcePool;

        public int PoolIncrement = 5;

        private int PoolSize
        {
            get { return _audioSourcePool.Count;  }
        }
        private int _objectCount;

        public static SoundManager Instance { get; private set; }

        private void Start()
        {

            if (Instance == null)
            {
                Instance = this;
                _audioSourcePool = new List<AudioSource>();
                _objectCount = 0;
            }
            else
            {
                Debug.LogError("A scene should only have one SoundManager");
            }
        }

        private bool FreePool()
        {
            for (var i = 0; i < _objectCount; ++i)
            {
                var source = _audioSourcePool[i];
                if (source.isPlaying)
                {
                    continue;
                }

                --_objectCount;
                var tmp = _audioSourcePool[_objectCount];
                _audioSourcePool[_objectCount] = source;
                _audioSourcePool[i] = tmp;
                --i;
            }

            return _objectCount < PoolSize;
        }

        private AudioSource GetNextInstance()
        {

            if (!(_objectCount < PoolSize || FreePool()))
            {

                for (int i = 0; i < PoolIncrement; ++i)
                {
                    var instance = Instantiate(AudioSourcePrefab);
                    instance.transform.parent = transform;
                    _audioSourcePool.Add(instance);
                }

            }

            return _audioSourcePool[_objectCount++];
        }

        public void Play(SoundBite bite)
        {
            if (bite == null || bite.Clip == null)
            {
                Debug.LogError("Asked to play null clip.");
                return;
            }

            var instance = GetNextInstance();
            instance.clip = bite.Clip;
            instance.volume = bite.Volume;
            instance.Play();

        }

    }
}

