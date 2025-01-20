using System.Collections;
using UnityEngine;


public class E4_DodgeState : DodgeState
{
    private Enemy_4 enemy;
    public E4_DodgeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, DodgeStateData stateData, Enemy_4 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDodgeOver)
        {

            if (isPlayerInMaxAgroRange )
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
