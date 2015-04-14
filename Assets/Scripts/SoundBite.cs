using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class SoundBite : Component
    {

        public AudioClip Clip;
        public float Volume;

        public void Play()
        {
            SoundManager.Instance.Play(this);
        }

    }
}
