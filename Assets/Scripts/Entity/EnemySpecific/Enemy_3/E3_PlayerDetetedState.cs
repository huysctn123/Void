using System.Collections;
using UnityEngine;


public class E3_PlayerDetetedState : PlayerDetectedState
{
    private Enemy_3 enemy;
    private int attackNumber;

    public E3_PlayerDetetedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, PlayerDetectedStateData stateData, Enemy_3 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        RandomAttack();
        if (enemy.healState.CheckIfCanHeal())
        {
            StateMachine.ChangeState(enemy.healState);
        }
        else if (performCloseRangeAction && attackNumber == 1)
        {
            StateMachine.ChangeState(enemy.meleeAttackState1);
        }
        else if (performCloseRangeAction && attackNumber == 2)
        {
            StateMachine.ChangeState(enemy.meleeAttackState2);
        }
        else if (performLongRangeAction)
        {
            StateMachine.ChangeState(enemy.chaseState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            Movement.Flip();
            StateMachine.ChangeState(enemy.moveState);
        }
    }
    private void RandomAttack()
    {
        var index = Random.Range(1, 3);
        attackNumber = index;
    }
}

