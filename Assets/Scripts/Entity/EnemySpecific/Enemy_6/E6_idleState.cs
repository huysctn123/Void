using System.Collections;
using UnityEngine;


public class E6_idleState : IdleState
{
    private Enemy_6 enemy;
    public E6_idleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, IdleStateData stateData, Enemy_6 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.stats.Health.OnCurrentValueDecrease += CurrentHealhChange;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.stats.Health.OnCurrentValueDecrease -= CurrentHealhChange;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange)
        {
            StateMachine.ChangeState(enemy.detectedState);
        }
        else
        if (isIdleTimeOver)
        {

            StateMachine.ChangeState(enemy.walkState);
        }
    }
    private void CurrentHealhChange()
    {
        StateMachine.ChangeState(enemy.detectedState);
    }
}
