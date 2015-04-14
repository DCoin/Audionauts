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

        public float Position
        {
            get { return (float)_audioSource.timeSamples /_audioSource.clip.samples; }
        }

        public float BeatsPerSecond
        {

            get
            {
                return BeatsPerLoop / _audioSource.clip.length;
            }

        }

        public float TimesPlayed
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
            if (!GetComponent<AudioSource>().isPlaying) return;

            if (GetComponent<AudioSource>().timeSamples < _lastSamples)
            {
                _timesPlayed++;
            }

            _lastSamples = GetComponent<AudioSource>().timeSamples;
        }
    }
}
