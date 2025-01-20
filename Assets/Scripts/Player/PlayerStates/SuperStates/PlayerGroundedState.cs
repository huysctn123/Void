using System;
using System.Collections;
using UnityEngine;
using Void.CoreSystem;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : 
        base(Player, stateMachine, playerData, animBoolName)
    {
    }
    protected Movement Movement => movement ? movement: core.GetCoreComponent(ref movement);
    private CollisionSenses CollisionSenses => collisionSenses ? collisionSenses : core.GetCoreComponent(ref collisionSenses);
    protected ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
    private ParticleManager particleManager;
    private Movement movement;
    private CollisionSenses collisionSenses;

    protected int xinput;
    protected int yinput;
    protected bool jumpInput;
    protected bool dashInput;
    protected bool grabInput;
    protected bool manaPotionInput;
    protected bool healPotionInput;


    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingledge;
    protected bool isTouchingCeiling;
  

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xinput = player.InputHandle.NormInputX;
        yinput = player.InputHandle.NormInputY;
        jumpInput = player.InputHandle.JumpInput;
        dashInput = player.InputHandle.DashInput;
        grabInput = player.InputHandle.GrabInput;
        manaPotionInput = player.InputHandle.ManaPotionInput;
        healPotionInput = player.InputHandle.HealPotionInput;

        if (player.InputHandle.AttackInputs[(int)CombatInputs.primary] && !isTouchingCeiling && player.PrimaryAttackState.CanTransitionToAttackState())
        {
            stateMachine.changeState(player.PrimaryAttackState);
        }
        else if (player.InputHandle.AttackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling && player.SecondaryAttackState.CanTransitionToAttackState())
        {
            stateMachine.changeState(player.SecondaryAttackState);
        }else if (jumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
        {
            stateMachine.changeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.changeState(player.InAirState);
        }
        else if(isTouchingWall && grabInput && isTouchingledge)
        {
            stateMachine.changeState(player.WallGrabState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            stateMachine.changeState(player.DashState);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void DoCheck()
    {
        base.DoCheck();
        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
            isTouchingCeiling = CollisionSenses.Ceiling;
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingledge = CollisionSenses.LedgeHorizontal;
        }
    }
}
