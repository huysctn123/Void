using System.Collections;
using UnityEngine;


public class Bat_WakeUpState : WakeUpState
{
    private Bat bat;
    public Bat_WakeUpState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Bat bat) : base(etity, stateMachine, animBoolName)
    {
        this.bat = bat;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            StateMachine.ChangeState(bat.flyIdleState);
        }
    }
}
