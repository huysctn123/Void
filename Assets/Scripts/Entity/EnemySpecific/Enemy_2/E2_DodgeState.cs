using System.Collections;
using UnityEngine;


public class E2_DodgeState : DodgeState
{
    private Enemy_2 enemy;
    public E2_DodgeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, DodgeStateData stateData, Enemy_2 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDodgeOver)
        {
            if (isPlayerInMaxAgroRange && performCloseRangeAction)
            {
                StateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if (isPlayerInMaxAgroRange && !performCloseRangeAction)
            {
                StateMachine.ChangeState(enemy.rangedAttackState);
            }
            else if (!isPlayerInMaxAgroRange)
            {
                StateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
}
