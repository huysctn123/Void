using System.Collections;
using UnityEngine;


public class WolfMeleeAttack4State : MeleeAttackState
{
    private Wolf enemy;
    public WolfMeleeAttack4State(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData, Wolf enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }
    public bool canMeleeAttack4 { get; private set; }
    private float lastAttackTime;
    public bool CheckIfCanAttack()
    {
        return canMeleeAttack4 && Time.time >= lastAttackTime + enemy.Attack4CoolDownTime;
    }
    public void ResetCanAttack() => canMeleeAttack4 = true;
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
        canMeleeAttack4 = false;
    }
}
