using System.Collections;
using UnityEngine;

namespace Void.CoreSystem
{
    public class AudioPlay : CoreComponent
    {
        [SerializeField] AudioSource audioSource;

        // Use this for initialization
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayAudioClip(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}