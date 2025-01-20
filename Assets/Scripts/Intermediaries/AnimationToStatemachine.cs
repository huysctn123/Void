using System;
using System.Collections;
using UnityEngine;
using Void.Weapons;


public class AnimationToStatemachine : MonoBehaviour
{
    public EntityState entityState;
    public AttackState attackState;
    public HealState healState;
    public WakeUpState wakeUpState;

    public event Action PlayAdioClip;
    public event Action PlayFX;
    public event Action WakeUpFinish;
    public event Action Healing;
    public event Action FinishHealing;
    public event Action AttackTrigger;
    public event Action AttackFinish;
    public event Action MovementStart;
    public event Action MovementStop;
    

    private void TriggerAttack()
    {
        AttackTrigger?.Invoke();
    } 
    private void FinishAttack()
    {
        AttackFinish?.Invoke();
    }
    private void FinishHeal()
    {
        FinishHealing?.Invoke();
    }
    private void FinishWakeUp()
    {
        WakeUpFinish?.Invoke();
    }
    private void StartHealing()
    {
        Healing?.Invoke();
    }
    private void SoundTrigger() => PlayAdioClip?.Invoke();

    private void FXTrigger() => PlayFX?.Invoke();
    private void StartMovement() => MovementStart?.Invoke();
    private void StopMovement() => MovementStop?.Invoke();

    private void SetParryWindowActive(int value)
    {
        attackState.SetParryWindowActive(Convert.ToBoolean(value));
    }
    private void DisableEntity()
    {
        this.gameObject.SetActive(false);
    }

}
