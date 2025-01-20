using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) :
        base(Player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            Movement?.SetVelocityY(playerData.WallClimbVelocity);
            Movement?.SetVelocityX(Movement.FacingDirection);
            if(yInput != 1)
            {
                stateMachine.changeState(player.WallGrabState);
            }
        }
    }
}

