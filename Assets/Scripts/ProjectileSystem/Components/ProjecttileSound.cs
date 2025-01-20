using System.Collections;
using UnityEngine;
using Void.Manager;
using Void.ProjectileSystem.Components;

namespace Void.ProjectileSystem
{
    public class ProjecttileSound : ProjectileComponent
    {
        public AudioClip Sound;

        public void PlayAudioClip()
        {
            Debug.Log("soundtrigger");
            SoundManager.Instance.PlaySoundFXClip(Sound);
        }
    }
}