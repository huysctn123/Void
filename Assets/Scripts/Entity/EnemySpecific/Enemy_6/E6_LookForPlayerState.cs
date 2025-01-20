using System.Collections;
using UnityEngine;


public class E6_LookForPlayerState : LookForPlayerState
{
    private Enemy_6 enemy;
    public E6_LookForPlayerState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, LookForPlayerStateData stateData, Enemy_6 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInManAgroRange)
        {
            StateMachine.ChangeState(enemy.detectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            StateMachine.ChangeState(enemy.walkState);
        }
    }
}
