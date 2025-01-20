using System.Collections;
using UnityEngine;

namespace Void.Machine
{
    public class SoundOnAction : MachineComponents
    {
        public AudioSource audioSource;

        protected override void Start()
        {
            base.Start();
            machine.OnEnter += PlayClip;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            machine.OnEnter -= PlayClip;
        }
        private void PlayClip()
        {
            audioSource.Play();
        }
    }
}