using System.Collections;
using UnityEngine;


public class E6_ChaseState : ChaseState
{
    private Enemy_6 enemy;

    public E6_ChaseState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, ChaseStateData stateData, Enemy_6 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            StateMachine.ChangeState(enemy.detectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(enemy.detectedState);
            }
            else
            {
                StateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
}
