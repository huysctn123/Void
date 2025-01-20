using System;
using UnityEngine;
using UnityEngine.Audio;


namespace Void.Manager
{
    public class SoundManager : VoidMonoBehaviour
    {
        public static SoundManager Instance;

        public AudioMixer mainMixer;

        public AudioSource Music;
        public AudioSource SFX;

        public float MasterVolume { get => masterVolume; private set => masterVolume = value; }
        public float MusicVolume { get => musicVolume; private set => musicVolume = value; }
        public float SFXVolume { get => sfxVolume; private set => sfxVolume = value; }

        [SerializeField] private float masterVolume = 1f;
        [SerializeField] private float musicVolume = 1f;
        [SerializeField] private float sfxVolume = 1f;

        protected override void Awake()
        {
            if (SoundManager.Instance != null) Debug.LogError("Only SoundManager allow");
            SoundManager.Instance = this;
        }
        protected override void LoadComponents()
        {
            base.LoadComponents();
        }
        public void PlaySoundFXClip(AudioClip clip)
        {
            SFX.clip = clip;
            SFX.Play();
        }
        public void PausePlay()
        {
            AudioListener.pause = true;
        }
        public void UnPausePlay()
        {
            AudioListener.pause = false;
        }
        public void SetMasterVolume(float amount)
        {
            masterVolume = amount;
            mainMixer.SetFloat("master", Mathf.Log10(masterVolume) * 20);
        }
        public void SetMusicVolume(float amount)
        {
            musicVolume = amount;
            mainMixer.SetFloat("music", Mathf.Log10(musicVolume) * 20);
        }
        public void SetSFXVolume(float amount)
        {
            SFXVolume = amount;
            mainMixer.SetFloat("sfx", Mathf.Log10(SFXVolume) * 20);
        }
    }
}
