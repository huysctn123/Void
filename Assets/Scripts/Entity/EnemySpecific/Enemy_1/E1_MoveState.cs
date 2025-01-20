using System.Collections;
using UnityEngine;


public class E1_MoveState : MoveState
{
    private Enemy_1 enemy;
    public E1_MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, MoveStateData stateData, Enemy_1 enemy) : base(etity, stateMachine, animBoolName, stateData)
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
            StateMachine.ChangeState(enemy.playerDetectedState);
        }else if(isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            StateMachine.ChangeState(enemy.idleState);
        }
    }
    private void CurrentHealhChange()
    {
        StateMachine.ChangeState(enemy.playerDetectedState);
    }

}
