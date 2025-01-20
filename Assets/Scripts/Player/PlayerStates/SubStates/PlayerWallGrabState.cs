using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;
    public PlayerWallGrabState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(Player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        holdPosition = player.transform.position;
        HoldPosition();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            HoldPosition();
            if(yInput > 0)
            {
                stateMachine.changeState(player.WallClimbState);
            }else if (yInput < 0 || !grabInput)
            {
                stateMachine.changeState(player.WallSlideState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void HoldPosition()
    {
        player.transform.position = holdPosition;

        Movement?.SetVelocityZero();
    }
}

