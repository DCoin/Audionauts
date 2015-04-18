using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class SoundBite : MonoBehaviour
    {

        public AudioClip Clip;
        public float Volume = 1;

        public AudioSource Play(float Beat = 0, bool pre = false)
        {
            return SoundManager.Instance.Play(this, Beat, pre);
        }

    }
}
