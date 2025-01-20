using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


    public class WakeUpState : EntityState
    {
    public WakeUpState(Entity entity, FiniteStateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        startTime = Time.time;
        entity.anim.SetTrigger(animBoolName);
        DoChecks();
        animToState.PlayAdioClip += SoundTrigger;
        animToState.PlayFX += FXTrigger;
        animToState.WakeUpFinish += FinishWakeUp;
    }

    public override void Exit()
    {
        animToState.PlayAdioClip -= SoundTrigger;
        animToState.WakeUpFinish -= FinishWakeUp;
        animToState.PlayFX -= FXTrigger;
    }

    public virtual void FinishWakeUp()
    {
        isAnimationFinished = true;
    }
}
