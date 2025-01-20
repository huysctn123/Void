using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Void.UI;


public class VolumeSetting : VoidMonoBehaviour
{
    public Volume volume;
    public ShowUI showUI;
    protected override void Awake()
    {
        base.Awake();

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (this.volume == null)
            this.volume = GameObject.Find("Global Volume").GetComponent<Volume>();
        this.showUI = GetComponent<ShowUI>();
    }

    protected override void Start()
    {
        base.Start();
    }

}
