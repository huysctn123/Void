using System;
using Unity.Mathematics;
using UnityEngine;
using Void.CoreSystem;
using Void.Manager;

[RequireComponent(typeof(AnimationToStatemachine))]

public class EntityState
{
    protected Entity entity;
    protected FiniteStateMachine StateMachine;
    protected Core core;
    protected AnimationToStatemachine animToState;
    protected AudioPlay audioPlay;

    public float startTime { get; protected set; }

    protected string animBoolName;

    protected bool isAnimationFinished;
    public EntityState(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.StateMachine = stateMachine;
        this.animBoolName = animBoolName;
        core = entity.Core;
        this.animToState = entity.atsm;
        audioPlay = core.GetCoreComponent<AudioPlay>();
    }
    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
        DoChecks();
        animToState.PlayAdioClip += SoundTrigger;
        animToState.PlayFX += FXTrigger;
        animToState.MovementStart += StartMovement;
        animToState.MovementStop += StopMovement;
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
        animToState.PlayAdioClip -= SoundTrigger;
        animToState.PlayFX -= FXTrigger;
        animToState.MovementStart -= StartMovement;
        animToState.MovementStop -= StopMovement;
    }

    public virtual void DoChecks()
    {
        
    }
    public virtual void SoundTrigger()
    {

    }
    public virtual void FXTrigger()
    {

    }
    public virtual void StartMovement()
    {

    }
    public virtual void StopMovement()
    {

    }


}

