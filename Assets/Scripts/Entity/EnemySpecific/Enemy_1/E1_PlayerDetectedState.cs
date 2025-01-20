using System.Collections;
using UnityEngine;


public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy_1 enemy;
    private int attackNumber;
    public E1_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, PlayerDetectedStateData stateData, Enemy_1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
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
            StateMachine.ChangeState(enemy.meleeAttack1State);
        }else if (performCloseRangeAction && attackNumber == 2)
        {
            StateMachine.ChangeState(enemy.meleeAttack2State);
        }else if (performLongRangeAction)
        {
            StateMachine.ChangeState(enemy.chargeState);
        }else if (!isPlayerInMaxAgroRange)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }else if (!isDetectingLedge)
        {
            Movement.Flip();
            StateMachine.ChangeState(enemy.moveState);
        }
    }
    private void RandomAttack()
    {
        var index = Random.Range(1, 3);
        attackNumber = index;
    }
}
