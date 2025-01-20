using System.Collections;
using UnityEngine;


public class WolfChaseState : ChaseState
{
    private Wolf enemy;
    public WolfChaseState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, ChaseStateData stateData, Wolf enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public bool canChase { get; private set; }
    private float lastAttackTime;
    public bool CheckIfCanAttack()
    {
        return canChase && Time.time >= lastAttackTime + enemy.ChaseCoolDownTime;
    }
    public void ResetCanAttack() => canChase = true;
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            if (enemy.meleeAttack1State.CheckIfCanAttack())
            {
                StateMachine.ChangeState(enemy.meleeAttack1State);
            }else if (enemy.meleeAttack2State2.CheckIfCanAttack())
            {
                StateMachine.ChangeState(enemy.meleeAttack2State1);
            }
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (isChargeTimeOver)
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
    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
    }

    public override void Enter()
    {
        base.Enter();
        canChase = false;
    }
}
