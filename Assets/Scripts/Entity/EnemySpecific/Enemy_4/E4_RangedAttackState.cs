using System.Collections;
using UnityEngine;


public class E4_RangedAttackState : RangedAttackState
{
    private Enemy_4 enemy;
    public E4_RangedAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, RangedAttackStateData stateData, Enemy_4 enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(enemy.rangedAttackState);
            }
            else
            {
                StateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

}
