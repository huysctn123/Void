using System.Collections;
using UnityEngine;


public class WolfMoveState : MoveState
{
    private Wolf enemy;
    public WolfMoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, MoveStateData stateData, Wolf enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    private bool canDetected;
    public override void DoChecks()
    {
        base.DoChecks();
        if (enemy.chaseState.CheckIfCanAttack() || enemy.meleeAttack3State.CheckIfCanAttack() 
            || enemy.rangedAttackState2.CheckIfAttack())
        {
            canDetected = true;
        }
    }

    public override void Enter()
    {
        base.Enter();
        enemy.stats.Health.OnCurrentValueDecrease += CurrentHealhChange;
        canDetected = false;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.stats.Health.OnCurrentValueDecrease -= CurrentHealhChange;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange && canDetected)
        {
            enemy.lookForPlayerState.UseLook();
            StateMachine.ChangeState(enemy.detectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            StateMachine.ChangeState(enemy.idleState);
        }else if (!isPlayerInMaxAgroRange && enemy.lookForPlayerState.canLook)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }
    private void CurrentHealhChange()
    {
        StateMachine.ChangeState(enemy.detectedState);
    }
}
