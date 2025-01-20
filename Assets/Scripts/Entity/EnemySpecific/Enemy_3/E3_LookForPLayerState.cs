using System.Collections;
using UnityEngine;

public class E3_LookForPLayerState : LookForPlayerState
{
    private Enemy_3 enemy;
    public E3_LookForPLayerState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, LookForPlayerStateData stateData, Enemy_3 enemy) : base(etity, stateMachine, animBoolName, stateData)
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
