using System;
using System.Collections;
using UnityEngine;
using Void.Manager;
using Void.Utilities;

[RequireComponent(typeof(AudioSource))]
public class ObjFXSound : VoidMonoBehaviour
{
    public AudioSource audioSource { get; private set; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        audioSource = GetComponent<AudioSource>();
    }

    public void AudioPLay()
    {
        audioSource.Play();
    }
}
