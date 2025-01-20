using System.Collections;
using UnityEngine;
using System;
using FirstGearGames.SmoothCameraShaker;


public class MutantJumpAttackState : MeleeAttackState
{
    private Mutant enemy;
    private float distanceFromPlayer;
    public MutantJumpAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData, Mutant enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        canAttack = false;
    }

    public override void Exit()
    {
        base.Exit();
        lastJumpAttackTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            StateMachine.ChangeState(enemy.detectedState);
        }
    }
    public bool canAttack { get; private set; }
    private float lastJumpAttackTime;
    public bool CheckIfCanAttack()
    {
        return canAttack && Time.time >= lastJumpAttackTime + enemy.JumpAttackCoolDownTime;
    }
    public void ResetCanAttack() => canAttack = true;
    public void SetJumpAttackPos()
    {
        distanceFromPlayer = enemy.PlayerCheckPos.position.x - enemy.transform.position.x;
    }
    public override void StartMovement()
    {
        base.StartMovement();
        Movement.addforceToEntity(new Vector2(distanceFromPlayer, stateData.Speed), ForceMode2D.Impulse);
    }
    public override void StopMovement()
    {
        base.StopMovement();
        Movement?.SetVelocityX(0f);
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();
        CameraShakerHandler.Shake(enemy.JumpAttackShakeData);
    }
}
