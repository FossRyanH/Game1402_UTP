using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine
{
    // Allows the "CurrentState" to be retrieved from anywhere in the state machine and set privately elsewhere.
    public IState CurrentState { get; private set; }

    // Sets the character's first state, whatever that may be.
    public void InitializeState(IState initialState)
    {
        if (CurrentState == null)
        {
            CurrentState = initialState;
            initialState.EnterState();
        }
    }

    // Transitions to the next state either beased on event or input.
    public void TransitionTo(IState nextState)
    {
        CurrentState.ExitState();
        CurrentState = nextState;
        nextState?.EnterState();
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdateState(Time.deltaTime);
        }
    }
}
