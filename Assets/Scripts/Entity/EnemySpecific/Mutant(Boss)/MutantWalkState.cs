using System.Collections;
using UnityEngine;


public class MutantWalkState : MoveState
{
    private Mutant enemy;
    public MutantWalkState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, MoveStateData stateData, Mutant enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange && enemy.detectedState.CheckCanDetected())
        {
            StateMachine.ChangeState(enemy.detectedState);
        }
        if (!isPlayerInMaxAgroRange)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }else if (isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            StateMachine.ChangeState(enemy.idleState);
        }
    }
}
