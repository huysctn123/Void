using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Void.CoreSystem;
using Void.Manager;

public class PlayerState
{
    protected Core core;

    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected string animBoolName;
    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float StartTime;
    public PlayerState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) 
    {
        this.player = Player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        core = Player.core; 
    }
    public virtual void Enter()
    {
        DoCheck();
        player.Anim.SetBool(animBoolName, true);
        StartTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
        //Debug.Log(animBoolName);
        
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }
    public virtual void DoCheck() { } 
    public virtual void AnimationTrigger() { }
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
    public virtual void FXTrigger() { }

    public virtual void SoundTrigger() { }

}
       

    

