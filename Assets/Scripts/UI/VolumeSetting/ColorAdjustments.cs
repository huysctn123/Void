using System.Collections;
using UnityEngine;
using Void.CoreSystem;


public class ColorAdjustments : VoLumeComponent
{
    [SerializeField] private UnityEngine.Rendering.Universal.ColorAdjustments color;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void HandleOnCloseUI()
    {
        base.HandleOnCloseUI();
        color.colorFilter.value  = Color.white;
    }

    protected override void HandleOnOpenUI()
    {
        base.HandleOnOpenUI();
        color.colorFilter.value = Color.gray;

    }

    protected override void Start()
    {
        if (volumeSetting.volume.profile.TryGet(out UnityEngine.Rendering.Universal.ColorAdjustments CA))
        {
            color = CA;
        } 
        base.Start();
        color.colorFilter.value = Color.white;

    }
}
