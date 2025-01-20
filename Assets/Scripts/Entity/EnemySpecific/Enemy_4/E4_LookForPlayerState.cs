using System.Collections;
using UnityEngine;


public class E4_LookForPlayerState : LookForPlayerState
{
    private Enemy_4 enemy;
    public E4_LookForPlayerState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, LookForPlayerStateData stateData, Enemy_4 enemy) : base(etity, stateMachine, animBoolName, stateData)
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
