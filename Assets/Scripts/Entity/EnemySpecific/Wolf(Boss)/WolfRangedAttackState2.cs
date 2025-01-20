using System.Collections;
using UnityEngine;


public class WolfRangedAttackState2 : RangedAttackState
{
    private Wolf enemy;
    public WolfRangedAttackState2(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, RangedAttackStateData stateData, Wolf enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }
    public bool canRangedAttack { get; private set; }
    private float lastAttackTime;
    public bool CheckIfAttack()
    {
        return canRangedAttack && Time.time >= lastAttackTime + enemy.RangedAttackCoolDownTime;
    }
    public void ResetCanAttack() => canRangedAttack = true;
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
    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
    }
    public override void Enter()
    {
        base.Enter();
        canRangedAttack = false;
    }
}
