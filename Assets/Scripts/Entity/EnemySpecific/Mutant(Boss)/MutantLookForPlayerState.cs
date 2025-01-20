using System.Collections;
using UnityEngine;


public class MutantLookForPlayerState : LookForPlayerState
{
    private Mutant enemy;
    public MutantLookForPlayerState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, LookForPlayerStateData stateData, Mutant enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInManAgroRange)
        {
            StateMachine.ChangeState(enemy.detectedState);
        }else if (isAllTurnsDone)
        {
            StateMachine.ChangeState(enemy.walkState);
        }
    }
}
