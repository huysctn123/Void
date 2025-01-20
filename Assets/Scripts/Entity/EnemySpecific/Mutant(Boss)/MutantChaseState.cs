using System.Collections;
using UnityEngine;

public class MutantChaseState : ChaseState
{
    private Mutant enemy;
    private bool canChase;
    private float lastTtimeChase;
    public MutantChaseState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, ChaseStateData stateData, Mutant enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        canChase = false;
    }

    public override void Exit()
    {
        base.Exit();
        lastTtimeChase = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            StateMachine.ChangeState(enemy.detectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            StateMachine.ChangeState(enemy.idleState);
        }
        else if(isChargeTimeOver)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);   
        }
    }
    public bool CheckIfCanChase()
    {
        return canChase && Time.time >= lastTtimeChase + enemy.ChaseCoolDownTime;
    }
    public bool ResetCanChase() => canChase = true;
}
