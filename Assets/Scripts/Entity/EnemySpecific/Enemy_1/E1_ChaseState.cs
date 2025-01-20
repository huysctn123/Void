using System.Collections;
using UnityEngine;

public class E1_ChaseState : ChaseState
{
    private Enemy_1 enemy;
    private int attackNumber;
    public E1_ChaseState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, ChaseStateData stateData, Enemy_1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        RandomAttack();     
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction && attackNumber == 1)
        {
            StateMachine.ChangeState(enemy.meleeAttack1State);
        }else if(performCloseRangeAction && attackNumber == 2)
        {
            StateMachine.ChangeState(enemy.meleeAttack2State);
        }else if (isDetectingWall || !isDetectingLedge)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                StateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
    private void RandomAttack()
    {
        var index = Random.Range(1, 3);
        attackNumber = index;
    }
}
