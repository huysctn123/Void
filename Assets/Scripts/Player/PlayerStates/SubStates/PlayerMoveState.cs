using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Void.Manager;


public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(Player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.CheckIfShouldFlip(xinput);
        player.Anim.SetFloat("VelocityY", Mathf.Round(Movement.CurrentVelocity.y));
        player.Anim.SetFloat("VelocityX", Mathf.Round(Mathf.Abs(Movement.CurrentVelocity.x)));
       

        if (!isExitingState)
        {
            if(xinput == 0)
            {
                stateMachine.changeState(player.IdleState);
            }else if(yinput == -1)
            {
                stateMachine.changeState(player.CrouchMoveState);
            }
         
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Movement?.SetVelocityX(playerData.MovementSpeed * Movement.FacingDirection);
    }
    public override void SoundTrigger()
    {
        base.SoundTrigger();
        SoundManager.Instance.PlaySoundFXClip(playerData.PlayerWalkSound);
    }
}


