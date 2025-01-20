using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class E5_PlayerDetectedState : PlayerDetectedState
{
    private Enemy_5 enemy;
    private float closeDistanceAction = 3f;
    private float Distance;
    public E5_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, PlayerDetectedStateData stateData, Enemy_5 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        Distance = Vector3.Distance(enemy.transform.position, enemy.playerPos.position);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if ((performCloseRangeAction || Distance <= closeDistanceAction) && enemy.meleeAttackState.CheckIfCanAttack())
        {
            StateMachine.ChangeState(enemy.meleeAttackState);
        }
        else
        {
            StateMachine.ChangeState(enemy.moveState);
        }
    }
}
