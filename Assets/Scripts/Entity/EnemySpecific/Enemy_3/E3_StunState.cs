using System.Collections;
using UnityEngine;


public class E3_StunState : StunState
{
    private Enemy_3 enemy;
    public E3_StunState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, StunStateData stateData, Enemy_3 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
