using System.Collections;
using UnityEngine;


public class E5_IdleState : IdleState
{
    private Enemy_5 enemy;
    public E5_IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, IdleStateData stateData, Enemy_5 enemy = null) : base(etity, stateMachine, animBoolName, stateData)
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
    private void CurrentHealhChange()
    {
        StateMachine.ChangeState(enemy.playerDetectedState);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMaxAgrorange)
        {
            StateMachine.ChangeState(enemy.playerDetectedState);
        }
    }
}
