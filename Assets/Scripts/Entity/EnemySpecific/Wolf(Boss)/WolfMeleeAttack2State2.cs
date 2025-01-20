using System.Collections;
using UnityEngine;


public class WolfMeleeAttack2State2 : MeleeAttackState
{
    private Wolf enemy; 
    public WolfMeleeAttack2State2(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData, Wolf enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    { 
        this.enemy = enemy; 
    }
    public bool canMeleeAttack2 { get; private set; }
    private float lastAttackTime;
    private float CoolDownTime = 15f;
    public bool CheckIfCanAttack()
    {
        return canMeleeAttack2 && Time.time >= lastAttackTime + enemy.Attack2CoolDownTime;
    }
    public void ResetCanAttack() => canMeleeAttack2 = true;

    public override void StartMovement()
    {
        base.StartMovement();
        Movement.SetVelocityX(stateData.Speed * Movement.FacingDirection);
    }
    public override void StopMovement()
    {
        base.StopMovement();
        Movement.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            StateMachine.ChangeState(enemy.detectedState);
        }
    }
    private void ChangedCoolDownTime()
    {
        if (enemy.Attack2CoolDownTime == CoolDownTime) return;
        enemy.Attack2CoolDownTime = CoolDownTime;
    }

    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
        ChangedCoolDownTime();
    }

    public override void Enter()
    {
        base.Enter();
        canMeleeAttack2 = false;
    }
}
