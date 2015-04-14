using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class SoundBite : MonoBehaviour
    {

        public AudioClip Clip;
        public float Volume = 1;

        public void Play()
        {
            SoundManager.Instance.Play(this);
        }

    }
}
