using System.Collections;
using UnityEngine;



public class WolfDetectedState : PlayerDetectedState
{
    private Wolf enemy;
    public WolfDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, PlayerDetectedStateData stateData, Wolf enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.meleeAttack1State.ResetCanAttack();
        enemy.meleeAttack2State2.ResetCanAttack();
        enemy.meleeAttack3State.ResetCanAttack();
        enemy.meleeAttack4State.ResetCanAttack();
        enemy.rangedAttackState2.ResetCanAttack();
        enemy.chaseState.ResetCanAttack();
        enemy.lookForPlayerState.ResetCanLook();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            if (enemy.meleeAttack1State.CheckIfCanAttack())
            {
                StateMachine.ChangeState(enemy.meleeAttack1State);
            }
            else if (enemy.meleeAttack2State2.CheckIfCanAttack())
            {
                StateMachine.ChangeState(enemy.meleeAttack2State1);
            }
            else if (enemy.meleeAttack4State.CheckIfCanAttack())
            {
                StateMachine.ChangeState(enemy.meleeAttack4State);
            }
            else if (enemy.rangedAttackState2.CheckIfAttack())
            {
                StateMachine.ChangeState(enemy.dodge2State);
            }

        }
        else if (isPlayerInMinAgroRange)
        {
            if (enemy.meleeAttack3State.CheckIfCanAttack())
            {
                StateMachine.ChangeState(enemy.dodge1State);
            }
            else if (enemy.rangedAttackState2.CheckIfAttack())
            {
                StateMachine.ChangeState(enemy.dodge2State);
            }
            else if (enemy.chaseState.CheckIfCanAttack())
            {
                StateMachine.ChangeState(enemy.chaseState);
            }
            else
            {
                StateMachine.ChangeState(enemy.moveState);
            }
        }
        else if (isPlayerInMaxAgroRange)
        {
            if (enemy.meleeAttack3State.CheckIfCanAttack())
            {
                StateMachine.ChangeState(enemy.dodge1State);
            }
            else if (enemy.rangedAttackState2.CheckIfAttack())
            {
                StateMachine.ChangeState(enemy.dodge2State);
            }
            else
            {
                StateMachine.ChangeState(enemy.chaseState);
            }
        }
        else if (!isPlayerInMaxAgroRange)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            Movement.Flip();
            StateMachine.ChangeState(enemy.moveState);
        }
    }
}
