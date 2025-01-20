using System.Collections;
using UnityEngine;


public class MutantMeleeComboAttackState : MeleeAttackState
{
    private Mutant enemy;
    public MutantMeleeComboAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData, Mutant enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }
    public bool canAttack { get; private set; }
    private float lastMeleeComboAttackTime;
    public bool CheckIfCanAttack()
    {
        return canAttack && Time.time >= lastMeleeComboAttackTime + enemy.MeleeComboAttackCoolDownTime;
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
        lastMeleeComboAttackTime = Time.time;
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
