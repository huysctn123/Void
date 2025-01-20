using System.Collections;
using UnityEngine;


public class E6_DetectedState : PlayerDetectedState
{
    private Enemy_6 enemy;
    public E6_DetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, PlayerDetectedStateData stateData, Enemy_6 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    private int attackNumber;
    public override void Enter()
    {
        base.Enter();
        RandomAttack();

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction && attackNumber == 1)
        {
            StateMachine.ChangeState(enemy.attack1State);
        }
        else if (performCloseRangeAction && attackNumber == 2)
        {
            StateMachine.ChangeState(enemy.attack2State);
        }
        else if (performLongRangeAction)
        {
            StateMachine.ChangeState(enemy.chaseState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            Movement.Flip();
            StateMachine.ChangeState(enemy.walkState);
        }
    }
    private void RandomAttack()
    {
        var index = Random.Range(1, 3);
        attackNumber = index;
    }
}
