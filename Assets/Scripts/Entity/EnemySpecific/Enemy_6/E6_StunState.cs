using System.Collections;
using UnityEngine;


public class E6_StunState : StunState
{
    private Enemy_6 enemy;
    public E6_StunState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, StunStateData stateData, Enemy_6 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
}
