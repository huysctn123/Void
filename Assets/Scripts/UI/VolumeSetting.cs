using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Void.Manager;

namespace Void.UI
{
    public class VolumeSetting : VoidMonoBehaviour
    {
        public Slider MasterSlider;
        public Slider MusicSlider;
        public Slider SFXSlider;

        protected override void Start()
        {
            SetMinVolume();
            SaveManager.Instance.OnLoadSoundVolume += UpdateValue;
        }
        protected override void LoadComponents()
        {
            base.LoadComponents();
        }
        private void SetMinVolume()
        {
            MasterSlider.minValue = 0.00001f;
            MusicSlider.minValue = 0.00001f;
            SFXSlider.minValue = 0.00001f;
        }
        public void SetMasterVolume()
        {
            SoundManager.Instance.SetMasterVolume(MasterSlider.value);
        }
        public void SetMusicVolume()
        {
            SoundManager.Instance.SetMusicVolume(MusicSlider.value);
        }
        public void SetSFXVolume()
        {
            SoundManager.Instance.SetSFXVolume(SFXSlider.value);
        }
        private void UpdateValue()
        {
            MasterSlider.value = SoundManager.Instance.MasterVolume;
            MusicSlider.value = SoundManager.Instance.MusicVolume;
            SFXSlider.value = SoundManager.Instance.SFXVolume;
        }
        protected override void OnDisable()
        {
            SaveManager.Instance.OnLoadSoundVolume -= UpdateValue;
        }


    }
}
