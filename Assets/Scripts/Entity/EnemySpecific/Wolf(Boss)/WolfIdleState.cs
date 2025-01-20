using System.Collections;
using UnityEngine;


public class WolfIdleState : IdleState
{
    private Wolf enemy;
    public WolfIdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, IdleStateData stateData, Wolf enemy) : base(etity, stateMachine, animBoolName, stateData)
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
        if (isPlayerInMaxAgrorange)
        {
            StateMachine.ChangeState(enemy.detectedState);
        }
        else if (isIdleTimeOver)
        {
            StateMachine.ChangeState(enemy.moveState);
        }
    }
    private void CurrentHealhChange()
    {
        StateMachine.ChangeState(enemy.detectedState);
    }
}
