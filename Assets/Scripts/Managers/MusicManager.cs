using UnityEngine;

namespace Assets.Scripts.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        public int BeatsPerLoop = 32;

        private AudioSource _audioSource;

        private int _timesPlayed;
        private int _lastSamples;

        public static MusicManager Instance { get; private set; }

        private float Position
        {
            get { return (float)_audioSource.timeSamples / _audioSource.clip.samples; }
        }

        private float TimesPlayed
        {
            get { return Position + _timesPlayed; }
        }

        public float BeatsPlayed
        {
            get { return TimesPlayed * BeatsPerLoop; }
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("A scene should only have one MusicManager");
            }
        }

        private void Update()
        {
			if (!_audioSource.isPlaying) return;

			if (_audioSource.timeSamples < _lastSamples)
            {
                _timesPlayed++;
            }

			_lastSamples = _audioSource.timeSamples;
        }
    }
}
