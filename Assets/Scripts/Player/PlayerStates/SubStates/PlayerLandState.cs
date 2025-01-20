using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Void.CoreSystem;
using Void.Manager;


public class PlayerLandState : PlayerGroundedState
{

    public PlayerLandState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(Player, stateMachine, playerData, animBoolName)
     {
     }

    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityY(0f);
        FXTrigger();
    }

    public override void LogicUpdate()
     {
        base.LogicUpdate();
        if (!isExitingState)
        {
          if(xinput != 0)
          {
              stateMachine.changeState(player.MoveState);
          }else if(isAnimationFinished)
          {
              stateMachine.changeState(player.IdleState);
          }
      }
     }
    public override void FXTrigger()
    {
        base.FXTrigger();
        var particleSystem = core.GetCoreComponent<ParticleManager>();
        particleSystem.StartParticles(playerData.LandFX, player.transform.position+  new Vector3(0, -2.5f, 0), Quaternion.identity);
    }
}

