using System.Collections;
using UnityEngine;


public class MutantIdleState : IdleState
{
    private Mutant enemy;
    public MutantIdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, IdleStateData stateData, Mutant enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMaxAgrorange && enemy.detectedState.CheckCanDetected())
        {
            StateMachine.ChangeState(enemy.detectedState);
        }
        else if (isIdleTimeOver)
        {
            StateMachine.ChangeState(enemy.walkState);
        }
    }
}
