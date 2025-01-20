using System.Collections;
using UnityEngine;


public class Bat_SleepState : IdleState
{
    private Bat bat;
    public Bat_SleepState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, IdleStateData stateData, Bat bat) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.bat = bat;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMaxAgrorange)
        {
            StateMachine.ChangeState(bat.wakeUpState);
        }
    }
}
