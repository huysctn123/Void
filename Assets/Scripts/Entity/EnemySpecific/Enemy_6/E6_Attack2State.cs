using System.Collections;
using UnityEngine;


public class E6_Attack2State : MeleeAttackState
{
    private Enemy_6 enemy;

    public E6_Attack2State(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData, Enemy_6 enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
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
                StateMachine.ChangeState(enemy.detectedState);
            }
            else
            {
                StateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
}
