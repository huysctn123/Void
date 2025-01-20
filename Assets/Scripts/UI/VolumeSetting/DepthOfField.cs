using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class DepthOfField : VoLumeComponent
{
    public UnityEngine.Rendering.Universal.DepthOfField depthOfField;

    private int forcuslenght;
    public int forcuslenghtAmount = 15;
    [Range(0.001f, 1f)]
    [SerializeField] private float Speed = 0.2f;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        if (volumeSetting.volume.profile.TryGet(out UnityEngine.Rendering.Universal.DepthOfField dph))
        {
            depthOfField = dph;
        }
        forcuslenght = 1;
        depthOfField.focalLength.value = 1;
    }

    private void Update()
    {
        depthOfField.focalLength.value = Mathf.Lerp(depthOfField.focalLength.value, forcuslenght, Speed);
    }
    protected override void HandleOnOpenUI()
    {
        base.HandleOnOpenUI();
        forcuslenght = forcuslenghtAmount;

    }
    protected override void HandleOnCloseUI()
    {
        base.HandleOnCloseUI();
        forcuslenght = 1;

    }
}
