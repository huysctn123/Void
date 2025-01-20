using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class Bat_FlyIdleState : IdleState
{
    private Bat bat;
    public Bat_FlyIdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, IdleStateData stateData, Bat bat) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.bat = bat;
    }
    public override void Enter()
    {
        base.Enter();
        bat.attackState.ResetCanAttack();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isIdleTimeOver)
        {
            StateMachine.ChangeState(bat.flyMoveState);
        }
    }
}
