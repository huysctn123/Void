using System.Collections;
using UnityEngine;


public class WolfMeleeAttack2State1 : MeleeAttackState
{
    private Wolf enemy;
    public WolfMeleeAttack2State1(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData, Wolf enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void StartMovement()
    {
        base.StartMovement();
        Movement.SetVelocityX(stateData.Speed * Movement.FacingDirection);
    }
    public override void StopMovement()
    {
        base.StopMovement();
        Movement.SetVelocityX(0f);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            StateMachine.ChangeState(enemy.meleeAttack2State2);
        }
    }
}
