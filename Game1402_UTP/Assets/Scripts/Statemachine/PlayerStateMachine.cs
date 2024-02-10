using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerStateMachine
{
    public IPlayerState CurrentState { get; private set; }

    // State references
    public PlayerIdleState IdleState;
    public PlayerMoveState MoveState;
    public PlayerJumpState JumpState;

    public event Action<IPlayerState> _stateChanged;

    public PlayerStateMachine(PlayerController player)
    {
        this.IdleState = new PlayerIdleState(player);
        this.JumpState = new PlayerJumpState(player);
        this.MoveState = new PlayerMoveState(player);
    }

    public void InitState(IPlayerState state)
    {
        CurrentState = state;
        state.EnterState();

        _stateChanged?.Invoke(state);
    }

    public void TransitionTo(IPlayerState nextState)
    {
        CurrentState.ExitState();
        CurrentState = nextState;
        nextState.EnterState();

        _stateChanged?.Invoke(nextState);
    }

    public void Update()
    {
        if (CurrentState != null)
            CurrentState.UpdateState();
    }
}
