using System.Collections;
using UnityEngine;


public class WolfDodgeBack2State : DodgeState
{
    private Wolf enemy;
    public WolfDodgeBack2State(Entity etity, FiniteStateMachine stateMachine, string animBoolName, DodgeStateData stateData, Wolf enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.anim.SetBool("ground", isGrounded);

        if (isDodgeOver)
        {
            StateMachine.ChangeState(enemy.rangedAttackState1);
        }
    }
}
