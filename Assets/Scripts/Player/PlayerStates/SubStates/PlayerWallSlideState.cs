using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWallSlideState : PlayerTouchingWallState
{  
    public PlayerWallSlideState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) :
    base(Player, stateMachine, playerData, animBoolName) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {         
            Movement?.SetVelocityY(-playerData.WallSildeVelocity);
            Movement?.SetVelocityX(Movement.FacingDirection * 0.01f);

            if(grabInput && yInput == 0)
            {
                stateMachine.changeState(player.WallGrabState);
            }
        }
    }
}

