using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Void.CoreSystem;
using Void.Manager;

public class HealState : EntityState
{
    protected Movement movement;
    protected Stats stats;

    protected HealStateData stateData;

    public HealState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, HealStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.movement = core.GetCoreComponent<Movement>();
        this.stats = core.GetCoreComponent<Stats>();
        this.stateData = stateData;
    }
    public override void Enter()
    {
        base.Enter();
        isAnimationFinished = false;
        movement?.SetVelocityX(0f);
        animToState.Healing += Healing;
        animToState.FinishHealing += FinishHeal;

    }

    public override void Exit()
    {
        base.Exit();
        animToState.Healing -= Healing;
        animToState.FinishHealing -= FinishHeal;
    }

    public virtual void FinishHeal()
    {
        isAnimationFinished = true;
    }
    public virtual void Healing()
    {
        stats.Health.Increase(stateData.amount);
    }
    public override void SoundTrigger()
    {
        base.SoundTrigger();
        audioPlay.PlayAudioClip(stateData.StateSound);
    }
}
