using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Void.CoreSystem;

public class PlayerDashState : PlayerAbilityState
{
    private Vector2 lastAIPos;
    public bool CanDash { get; private set; }

    private float lastDashTime;
    public PlayerDashState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(Player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        CanDash = false;
        Movement?.SetGravity(0f);
        Movement?.SetVelocityY(0f);
        player.InputHandle.UseDashInput();
        StartDashFX();
        DeactiveCombatReceiver();
        
    }

    public override void Exit()
    {
        base.Exit();
        Movement?.SetGravity(playerData.DefaultGravity);
        ActiveCombatReceiver();


    }

    private void StartDashFX()
    {
        var DashFX = GameObject.Instantiate(player.DashParticle, player.FxPos.position, player.FxPos.transform.rotation);
        DashFX.transform.SetParent(player.FxPos);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
           
            if (Time.time >= StartTime + playerData.DashTime)
            {
                player.RB.drag = 0f;
                lastDashTime = Time.time;
                isAbilityDone = true;
            }
            else
            {
                Movement?.SetVelocityX(playerData.DashVelocity * Movement.FacingDirection);
                CheckIfShouldPlaceAfterImage();
            }

        }
       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.DashCooldown;
    }
    public void ResetCanDash() => CanDash = true;

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAIPos = player.transform.position;
    }
    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(player.transform.position, lastAIPos) >= playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }
    private void ActiveCombatReceiver()
    {
        player.combatCollider.CombatBoxCollider.enabled = true;
        Physics2D.IgnoreLayerCollision(6, 13, false);
    }
    private void DeactiveCombatReceiver()
    {
        player.combatCollider.CombatBoxCollider.enabled = false;
        Physics2D.IgnoreLayerCollision(6, 13, true);

    }
}

