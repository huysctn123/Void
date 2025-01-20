using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Void.UI;


[RequireComponent(typeof(VolumeSetting))]
public abstract class VoLumeComponent : VoidMonoBehaviour
{
    protected VolumeSetting volumeSetting;
    protected ShowUI ShowUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        volumeSetting = GetComponent<VolumeSetting>();
        ShowUI = volumeSetting.showUI;
    }
    protected virtual void HandleOnOpenUI()
    {

    }
    protected virtual void HandleOnCloseUI()
    {

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ShowUI.OnOpenUI -= HandleOnOpenUI;
        ShowUI.OnCloseUI -= HandleOnCloseUI;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ShowUI.OnOpenUI += HandleOnOpenUI;
        ShowUI.OnCloseUI += HandleOnCloseUI;
    }

}
