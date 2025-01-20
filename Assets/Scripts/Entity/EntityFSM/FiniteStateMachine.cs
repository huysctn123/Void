using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FiniteStateMachine
{
    public EntityState CurrentState { get; private set; }

    public void Initialize(EntityState StartingState)
    {
        CurrentState = StartingState;
        CurrentState.Enter();

    }
    public void ChangeState(EntityState NewState)
    {
        CurrentState.Exit();
        CurrentState = NewState;
        CurrentState.Enter();
    }
}

