using System.Collections;
using UnityEngine;
using Void.CoreSystem;


public class MutantDieState : EntityState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private Mutant enemy;
    public MutantDieState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Mutant enemy) : base(entity, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        Movement?.SetVelocityX(0f);
        startTime = Time.time;
        entity.anim.SetTrigger(animBoolName);
        DoChecks();
        animToState.PlayAdioClip += SoundTrigger;
        animToState.PlayFX += FXTrigger;
    }
}
