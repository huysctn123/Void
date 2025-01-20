using System.Collections;
using UnityEngine;


public class WolfLookForPlayerState : LookForPlayerState
{
    private Wolf enemy;
    public WolfLookForPlayerState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, LookForPlayerStateData stateData, Wolf enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public bool canLook { get; private set; }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInManAgroRange)
        {
            StateMachine.ChangeState(enemy.detectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            StateMachine.ChangeState(enemy.moveState);
        }
    }
    public void UseLook() => canLook = false;
    public void ResetCanLook() => canLook = true;
}
