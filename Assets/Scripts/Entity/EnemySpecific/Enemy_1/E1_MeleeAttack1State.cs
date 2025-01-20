
using UnityEngine;

public class E1_MeleeAttack1State : MeleeAttackState
{
    private Enemy_1 enemy;

    public E1_MeleeAttack1State(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData, Enemy_1 enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
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
                StateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                StateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
    public override void StartMovement()
    {
        base.StartMovement();
    }
    public override void StopMovement()
    {
        base.StopMovement();
    }


}

