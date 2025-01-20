using System.Collections;
using UnityEngine;


public class E4_PlayerDetectedState : PlayerDetectedState
{
    private Enemy_4 enemy;
    public E4_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, PlayerDetectedStateData stateData, Enemy_4 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            if (Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown && performCloseRangeAction)
            {
                StateMachine.ChangeState(enemy.dodgeState);
            }
            else
            {
                StateMachine.ChangeState(enemy.idleState);
            }

        }
        else if (isPlayerInMaxAgroRange)
        {
            StateMachine.ChangeState(enemy.rangedAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }
}
