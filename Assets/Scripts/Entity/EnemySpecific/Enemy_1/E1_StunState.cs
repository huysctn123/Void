using System.Collections;
using UnityEngine;


public class E1_StunState : StunState
{
    private Enemy_1 enemy;
    public E1_StunState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, StunStateData stateData, Enemy_1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.StunEffect.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.StunEffect.gameObject.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isStunTimeOver)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }
}
