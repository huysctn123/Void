using System.Collections;
using UnityEngine;


public class E5_MeleeAttackState : MeleeAttackState
{
    private Enemy_5 enemy;
    public E5_MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData, Enemy_5 enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }   
    public bool canAttack { get; private set; }
    private float lastAttackTime;
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                StateMachine.ChangeState(enemy.moveState);
            }
        }
    }
    public bool CheckIfCanAttack()
    {
        return canAttack && Time.time >= lastAttackTime + enemy.attackCoolDownTime;
    }
    public void ResetCanAttack() => canAttack = true;

    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
    }

    public override void Enter()
    {
        base.Enter();
        canAttack = false;
    }
}
