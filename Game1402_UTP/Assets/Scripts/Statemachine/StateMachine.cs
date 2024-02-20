using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine
{
    // Allows the "CurrentState" to be retrieved from anywhere in the state machine and set priovately elsewhere.
    public IState CurrentState { get; private set; }

    // Event to notify of state changes.
    public event Action<IState> _stateChanged;

    // Sets the character's first state, whatever that may be.
    public void InitializeState(IState initialState)
    {
        CurrentState = initialState;
        initialState.EnterState();

        _stateChanged?.Invoke(initialState);
    }

    // Transitions to the next state either beased on event or input.
    public void TransitionTo(IState nextState)
    {
        CurrentState.ExitState();
        CurrentState = nextState;
        nextState?.EnterState();

        _stateChanged?.Invoke(nextState);
    }

    public void Update()
    {
        CurrentState?.UpdateState(Time.deltaTime);
    }
}
