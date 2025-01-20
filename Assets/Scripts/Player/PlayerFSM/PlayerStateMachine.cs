using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState CurrentState { get; private set; }

    public void InitializeState(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void changeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}

