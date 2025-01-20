using System.Collections;
using UnityEngine;
using Void.CoreSystem;


public class PlayerDieState : PlayerState
{
    private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);

    private ParticleManager particleManager;

    private Movement Movement => movement ? movement : core.GetCoreComponent(ref movement);
    private Movement movement;

    private Vector3 RelativeFXPos = new Vector3(-1.75f, -2.45f, 0f);
    public PlayerDieState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(Player, stateMachine, playerData, animBoolName)
    {

    }

    public override void Enter()
    {
        //DoCheck();
        player.Anim.SetTrigger(animBoolName);
        StartTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
        //Debug.Log(animBoolName);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        movement.SetVelocityX(0f);
        
    }
}
