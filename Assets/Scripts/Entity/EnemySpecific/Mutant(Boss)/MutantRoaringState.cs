using System.Collections;
using UnityEngine;


public class MutantRoaringState : IdleState
{
    private Mutant enemy;

    public MutantRoaringState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, IdleStateData stateData, Mutant enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isIdleTimeOver)
        {
            StateMachine.ChangeState(enemy.idleState);
        }
    }
}
