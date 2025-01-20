using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;


public class MachineAnimationEventHandler : MonoBehaviour
{
    public event Action OnTrigger;
    public event Action OnFinish;
    public event Action OnStartAction;
    public event Action OnStopAction;


    private void AnimationFinishedTrigger() => OnFinish?.Invoke();
    private void AnimationTrigger() => OnTrigger?.Invoke();
    private void StartAction() => OnStartAction?.Invoke();
    private void StopAction() => OnStopAction?.Invoke();
}
