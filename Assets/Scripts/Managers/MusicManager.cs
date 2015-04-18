using UnityEngine;

namespace Assets.Scripts.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        public int BeatsPerLoop = 32;

        public Section StartAtSection;

        private AudioSource _audioSource;

        private int _timesPlayed;
        private int _lastSamples;

        private const int XFRAMES = 10;
        private const float ERRORCORRECTION = 0.2f;
        private int[] _lastXFrames = new int[XFRAMES];
        private int _currentFrame = 0;
        private int _sumLastXFrames = 0;
        private float _smoothFramesPlayed = 0;
        // private Vector3 lastPoint;
        // private Vector3 lastAVGPoint;

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

        private int FramesPlayed
        {
            get { return _audioSource.timeSamples + _timesPlayed * _audioSource.clip.samples; }
        }

        private float SmoothTimesPlayed
        {
            get { return _smoothFramesPlayed / _audioSource.clip.samples; }
        }
        
        public float SmoothBeatsPlayed
        {
            get { return SmoothTimesPlayed * BeatsPerLoop; }
        }

        // The time the clip base clip has been playing (Used for comparing to the general dsp time scale)
        private float dspTimePlayed
        {
            get { return _timesPlayed * _audioSource.clip.length + _audioSource.time; }
        }

        // The time when the clip started playing on the general dsp time scale
        private double dspStart
        {
            get { return AudioSettings.dspTime - dspTimePlayed; }
        }

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("A scene should only have one MusicManager");
            }

            _audioSource = GetComponent<AudioSource>();



            if (StartAtSection != null) {
                float startBeat = StartAtSection.transform.localPosition.z * 4;
                while (startBeat > BeatsPerLoop) {
                    startBeat -= BeatsPerLoop;
                    _timesPlayed++;
                }

                var prog = startBeat/BeatsPerLoop;
                _audioSource.timeSamples = (int) (prog*_audioSource.clip.samples);


            }


            // The value of this should lie between 0 and 10000 and only matters for the first XFRAMES frames
            var sampleGuess = 1000;
            for (int i = 0; i < XFRAMES; i++) {
                _lastXFrames[i] = sampleGuess;
            }
            _sumLastXFrames = sampleGuess * XFRAMES;
        }

        private void Update()
        {
			if (!_audioSource.isPlaying) return;

            // Subtract the oldest frames length from the sum
            _sumLastXFrames -= _lastXFrames[_currentFrame];
            // Replace its value with the length of the current frame
            // and take into account that the clip is looping
            if (_audioSource.timeSamples < _lastSamples) _lastXFrames[_currentFrame] = _audioSource.timeSamples + _audioSource.clip.samples - _lastSamples;
            else _lastXFrames[_currentFrame] = _audioSource.timeSamples - _lastSamples;
            // Add the new length to the sum
            _sumLastXFrames += _lastXFrames[_currentFrame];

            // Add the predicted change
            _smoothFramesPlayed += _sumLastXFrames / XFRAMES;
            // Correct for errors
            _smoothFramesPlayed += (FramesPlayed - _smoothFramesPlayed) * ERRORCORRECTION;
            // TODO Make sure this correction does not make it go backwards

            /* Code for visualising the smoothing
            var point = new Vector3(Time.frameCount, BeatsPlayed*10);
            Debug.DrawLine(lastPoint, point, Color.red, 1000);
            lastPoint = point;
            point = new Vector3(Time.frameCount, AVGBeatsPlayed*10);
            Debug.DrawLine(lastAVGPoint, point, Color.green, 1000);
            lastAVGPoint = point;
            */

            // Prepare for the next frame
            _currentFrame = (_currentFrame + 1) % XFRAMES;


            // Start placement code


			if (_audioSource.timeSamples < _lastSamples)
            {
                _timesPlayed++;
            }
            
            _lastSamples = _audioSource.timeSamples;
        }

        private float BeatsInSeconds(float Beats)
        {
            return _audioSource.clip.length / BeatsPerLoop * Beats;
        }

        public double BeatsOnDSP(float Beats)
        {
            return dspStart + BeatsInSeconds(Beats);
        }
    }
}
