using System.Collections;
using UnityEngine;


public class E3_ChaseState : ChaseState
{
    private Enemy_3 enemy;
    private int attackNumber;

    public E3_ChaseState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, ChaseStateData stateData, Enemy_3 enemy = null) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            enemy.stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
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
}
