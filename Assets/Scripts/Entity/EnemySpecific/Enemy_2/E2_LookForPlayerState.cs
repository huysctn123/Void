using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class E2_LookForPlayerState : LookForPlayerState
{
    private Enemy_2 enemy;
    public E2_LookForPlayerState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, LookForPlayerStateData stateData, Enemy_2 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange)
        {
            StateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            StateMachine.ChangeState(enemy.moveState);
        }
    }
}

