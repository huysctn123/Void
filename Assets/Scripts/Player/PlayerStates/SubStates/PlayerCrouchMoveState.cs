using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(Player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetColliderHeight(playerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            Movement?.SetVelocityX(playerData.CrouchSpeed * Movement.FacingDirection);
            Movement.CheckIfShouldFlip(xinput);
            if(yinput != -1 && !isTouchingCeiling)
            {
                stateMachine.changeState(player.IdleState);
            }else if(xinput == 0 )
            {
                stateMachine.changeState(player.CrouchIdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
