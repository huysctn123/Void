using System.Collections;
using UnityEngine;


public class E2_StunState : StunState
{
    private Enemy_2 enemy;
    public E2_StunState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, StunStateData stateData, Enemy_2 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                StateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if (isPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(enemy.rangedAttackState);
            }
            else
            {
                enemy.lookForPlayerState.SetTurnImmediately(true);
                StateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

}
