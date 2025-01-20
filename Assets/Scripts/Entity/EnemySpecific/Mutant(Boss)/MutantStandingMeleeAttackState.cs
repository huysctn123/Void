using System.Collections;
using UnityEngine;
using Void.Projectiles;


public class MutantStandingMeleeAttackState : MeleeAttackState
{
    private Mutant enemy;
    public MutantStandingMeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData, Mutant enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }
    public bool canAttack { get; private set; }
    private float lastAttackTime;
    public bool CheckIfCanAttack()
    {
        return canAttack && Time.time >= lastAttackTime + enemy.StandingMeleeAttackBackhandCoolDownTime;
    }
    public void ResetCanAttack() => canAttack = true;

    public override void Enter()
    {
        base.Enter();
        canAttack = false;
    }
    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            StateMachine.ChangeState(enemy.detectedState);
        }
    }
}
