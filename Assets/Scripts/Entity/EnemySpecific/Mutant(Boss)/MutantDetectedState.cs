using System.Collections;
using UnityEngine;


public class MutantDetectedState : PlayerDetectedState
{
    private Mutant enemy;
    public MutantDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, PlayerDetectedStateData stateData, Mutant enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.swipingState.ResetCanAttack();
        enemy.meleeComboAttackState.ResetCanAttack();
        enemy.standingMeleeAttackState.ResetCanAttack();
        enemy.jumpAttackState.ResetCanAttack();
        enemy.chaseState.ResetCanChase();

    }
    public bool CheckCanDetected()
    {
        return enemy.swipingState.CheckIfCanAttack() || enemy.standingMeleeAttackState.CheckIfCanAttack()
            || enemy.meleeComboAttackState.CheckIfCanAttack() || enemy.jumpAttackState.CheckIfCanAttack();
    }
    public bool CheckCanChaseAttack()
    {
        return enemy.swipingState.CheckIfCanAttack() || enemy.standingMeleeAttackState.CheckIfCanAttack()
            || enemy.meleeComboAttackState.CheckIfCanAttack();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            if (enemy.swipingState.CheckIfCanAttack())
            {
                StateMachine.ChangeState(enemy.swipingState);
            }
            else if (enemy.standingMeleeAttackState.CheckIfCanAttack())
            {
                StateMachine.ChangeState(enemy.standingMeleeAttackState);
            }
            else if (enemy.meleeComboAttackState.CheckIfCanAttack())
            {
                StateMachine.ChangeState(enemy.meleeComboAttackState);
            }
        }
        else if (isPlayerInMaxAgroRange)
        {
            if(enemy.chaseState.CheckIfCanChase() && CheckCanChaseAttack())
            {
                StateMachine.ChangeState(enemy.chaseState);
            }else if (enemy.jumpAttackState.CheckIfCanAttack())
            {
                enemy.jumpAttackState.SetJumpAttackPos();
                StateMachine.ChangeState(enemy.jumpAttackState);
            }
        }
        else if(!isPlayerInMaxAgroRange)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else
        {
            StateMachine.ChangeState(enemy.walkState);
        }
    }
}
