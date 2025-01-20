using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(Player, stateMachine, playerData, animBoolName)
    {
    }
   
    public override void Enter()
    {   
        base.Enter();
        Movement?.SetVelocityX(0f);
      
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (xinput != 0f)
        {
            stateMachine.changeState(player.MoveState);
        } else if (yinput == -1)
        {
            stateMachine.changeState(player.CrouchIdleState);
        } else if (manaPotionInput && player.UseManaPotionState.CheckCanUseManaPotion())
        {
            stateMachine.changeState(player.UseManaPotionState);
        } else if (healPotionInput && player.UseHealPotionState.CheckCanUseHealPotion())
        {
            stateMachine.changeState(player.UseHealPotionState);
        }
        
       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Movement.SetVelocityX(0f);
    }
}
