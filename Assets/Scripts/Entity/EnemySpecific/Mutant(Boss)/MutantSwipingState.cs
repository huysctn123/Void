using System.Collections;
using UnityEngine;
using Void.Projectiles;

public class MutantSwipingState : MeleeAttackState
{
    private Mutant enemy;
    public MutantSwipingState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData, Mutant enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }
    protected Projectile projectileScript;
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        canAttack = false;
        enemy.standingMeleeAttackState.ResetCanAttack();
    }

    public override void Exit()
    {
        base.Exit();
        lastSwingTime = Time.time;
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
    private float lastSwingTime;
    public bool CheckIfCanAttack()
    {
        return canAttack && Time.time >= lastSwingTime + enemy.SwipingCoolDownTime;
    }
    public void ResetCanAttack() => canAttack = true;
}
